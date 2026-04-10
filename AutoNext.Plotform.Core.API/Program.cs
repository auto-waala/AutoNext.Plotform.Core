using AutoNext.Plotform.Core.API.Data.Context;
using AutoNext.Plotform.Core.API.Data.UnitOfWork;
using AutoNext.Plotform.Core.API.Middlewares;
using AutoNext.Plotform.Core.API.Services;
using DbUp;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithThreadId()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        npgsqlOptions =>
        {
            npgsqlOptions.EnableRetryOnFailure(3);
            npgsqlOptions.CommandTimeout(30);
        }));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ILocationService, LocationService>();

builder.Services.AddAutoMapper(typeof(Program));

var redisConnection = builder.Configuration.GetConnectionString("Redis");
if (!string.IsNullOrEmpty(redisConnection))
{
    builder.Services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = redisConnection;
        options.InstanceName = "AutoNext_";
    });
}
else
{
    builder.Services.AddDistributedMemoryCache();
    Log.Warning("Redis not configured — using in-memory cache.");
}

builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

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

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection")!);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddResponseCaching();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AutoNext Platform Core API v1");
    c.RoutePrefix = "swagger";
});

app.UseSerilogRequestLogging();
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseCors("AllowAll");
app.UseResponseCaching();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health");

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

try
{
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

    Log.Information("All database scripts applied successfully.");
}
catch (Exception ex)
{
    Log.Error(ex, "Database deployment failed.");
}

Log.Information("Application starting — http://localhost:5096/swagger");
Log.CloseAndFlush();

app.Run();