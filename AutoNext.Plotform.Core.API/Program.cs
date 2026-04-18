using AutoNext.Plotform.Core.API.Data.Context;
using AutoNext.Plotform.Core.API.Data.UnitOfWork;
using AutoNext.Plotform.Core.API.Mappings;
using AutoNext.Plotform.Core.API.Middlewares;
using AutoNext.Plotform.Core.API.Services;
using DbUp;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithThreadId()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        npgsqlOptions =>
        {
            npgsqlOptions.EnableRetryOnFailure(3);
            npgsqlOptions.CommandTimeout(30);
        }));

// Add Redis Cache
var redisConnection = builder.Configuration.GetConnectionString("Redis");
if (!string.IsNullOrEmpty(redisConnection) && !redisConnection.Contains("localhost"))
{
    builder.Services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = redisConnection;
        options.InstanceName = "AutoNext_Core_";
    });
}
else
{
    builder.Services.AddDistributedMemoryCache();
    Log.Warning("Redis not configured for Core API — using in-memory cache.");
}

// Register Services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IVehicleTypeService, VehicleTypeService>();
builder.Services.AddScoped<IFuelTypeService, FuelTypeService>();
builder.Services.AddScoped<ITransmissionService, TransmissionService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IColorService, ColorService>();
builder.Services.AddScoped<IDocumentTypeService, DocumentTypeService>();
builder.Services.AddScoped<IFeatureService, FeatureService>();
builder.Services.AddScoped<IInspectionChecklistService, InspectionChecklistService>();
builder.Services.AddScoped<IPaymentMethodService, PaymentMethodService>();
builder.Services.AddScoped<IVehicleModelService, VehicleModelService>();
builder.Services.AddScoped<IServiceTypeService, ServiceTypeService>();
builder.Services.AddScoped<IShippingOptionService, ShippingOptionService>();
builder.Services.AddScoped<ITaxRateService, TaxRateService>();
builder.Services.AddScoped<ITitleTypeService, TitleTypeService>();
builder.Services.AddScoped<IVehicleVariantService, VehicleVariantService>();
builder.Services.AddScoped<IVehicleConditionService, VehicleConditionService>();
builder.Services.AddScoped<IWarrantyTypeService, WarrantyTypeService>();

// Add AutoMapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();
    cfg.AddProfile<BrandProfile>();
    cfg.AddProfile<CategoryProfile>();
    cfg.AddProfile<ColorProfile>();
    cfg.AddProfile<DocumentTypeProfile>();
    cfg.AddProfile<FeatureProfile>();
    cfg.AddProfile<InspectionChecklistProfile>();
    cfg.AddProfile<PaymentMethodProfile>();
    cfg.AddProfile<VehicleModelProfile>();
    cfg.AddProfile<ServiceTypeProfile>();
    cfg.AddProfile<ShippingOptionProfile>();
    cfg.AddProfile<TaxRateProfile>();
    cfg.AddProfile<TitleTypeProfile>();
    cfg.AddProfile<VehicleVariantProfile>();
    cfg.AddProfile<VehicleConditionProfile>();
    cfg.AddProfile<WarrantyTypeProfile>();
});

builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Add API Versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "AutoNext Platform Core API",
        Version = "v1",
        Description = "Core data management for AutoNext vehicle marketplace",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "AutoNext Team",
            Email = "support@autonext.com"
        }
    });
    c.EnableAnnotations();
});

// Add Health Checks
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection")!);

// ============ DYNAMIC CORS CONFIGURATION ============
// Load allowed origins from configuration
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
                     ?? Array.Empty<string>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("ConfiguredCorsPolicy", policy =>
    {
        if (allowedOrigins.Any())
        {
            policy.WithOrigins(allowedOrigins)
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials();
        }
        else if (builder.Environment.IsDevelopment())
        {
            // Fallback for local development
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        }
    });
});

builder.Services.AddResponseCaching();

var app = builder.Build();

// Configure pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AutoNext Platform Core API v1");
        c.RoutePrefix = "swagger";
    });
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AutoNext Platform Core API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseSerilogRequestLogging();
app.UseMiddleware<GlobalExceptionMiddleware>();

// Use CORS with the configured policy
app.UseCors("ConfiguredCorsPolicy");

app.UseResponseCaching();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health");

// Run database scripts after app starts
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

app.Lifetime.ApplicationStarted.Register(() =>
{
    try
    {
        Log.Information($"Running database migrations for {app.Environment.EnvironmentName} environment...");

        Log.Information("Running PreDeployment scripts...");
        var pre = DeployChanges.To
            .PostgresqlDatabase(connectionString)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(),
                s => s.Contains(".Scripts.PreDeployment."))
            .WithTransactionPerScript()
            .LogToConsole()
            .Build();
        var preResult = pre.PerformUpgrade();
        if (!preResult.Successful) throw new Exception($"PreDeployment failed: {preResult.Error}");

        Log.Information("Running Migration scripts...");
        var migrate = DeployChanges.To
            .PostgresqlDatabase(connectionString)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(),
                s => s.Contains(".Scripts.Migrations."))
            .WithTransaction()
            .LogToConsole()
            .Build();
        var migrateResult = migrate.PerformUpgrade();
        if (!migrateResult.Successful) throw new Exception($"Migration failed: {migrateResult.Error}");

        Log.Information("Running StoredProcs scripts...");
        var procs = DeployChanges.To
            .PostgresqlDatabase(connectionString)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(),
                s => s.Contains(".Scripts.StoredProcs."))
            .WithTransactionPerScript()
            .LogToConsole()
            .Build();
        var procsResult = procs.PerformUpgrade();
        if (!procsResult.Successful) throw new Exception($"StoredProcs failed: {procsResult.Error}");

        Log.Information("Running Functions scripts...");
        var functions = DeployChanges.To
            .PostgresqlDatabase(connectionString)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(),
                s => s.Contains(".Scripts.Functions."))
            .WithTransactionPerScript()
            .LogToConsole()
            .Build();
        var functionsResult = functions.PerformUpgrade();
        if (!functionsResult.Successful) throw new Exception($"Functions failed: {functionsResult.Error}");

        Log.Information("Running PostDeployment scripts...");
        var post = DeployChanges.To
            .PostgresqlDatabase(connectionString)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly(),
                s => s.Contains(".Scripts.PostDeployment."))
            .WithTransactionPerScript()
            .LogToConsole()
            .Build();
        var postResult = post.PerformUpgrade();
        if (!postResult.Successful) throw new Exception($"PostDeployment failed: {postResult.Error}");

        Log.Information("All database scripts applied successfully for Core API.");
    }
    catch (Exception ex)
    {
        Log.Error(ex, "Database deployment failed for Core API.");
    }
});

// Flush logs cleanly on shutdown
app.Lifetime.ApplicationStopped.Register(Log.CloseAndFlush);

Log.Information($"Core API starting in {app.Environment.EnvironmentName} environment");
app.Run();