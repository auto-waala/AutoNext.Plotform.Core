using AutoNext.Plotform.Core.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoNext.Plotform.Core.API.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Location> Locations { get; set; }
        public DbSet<CityArea> CityAreas { get; set; }
        public DbSet<FuelType> FuelTypes { get; set; }
        public DbSet<Transmission> Transmission { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<InspectionChecklist> InspectionChecks { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<ShippingOption> ShippingOptions { get; set; }
        public DbSet<TaxRate> TaxRates { get; set; }
        public DbSet<TitleType> TitleTypes { get; set; }
        public DbSet<VehicleVariant> VehicleVariants { get; set; }
        public DbSet<VehicleCondition> VehicleConditions { get; set; }
        public DbSet<WarrantyType> WarrantyTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ─── Default schema ───────────────────────────────────────────────
            modelBuilder.HasDefaultSchema("public");

            // ─── VehicleType ──────────────────────────────────────────────────
            modelBuilder.Entity<VehicleType>(entity =>
            {
                entity.ToTable("vehicle_types", "public");

                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.Code).IsUnique().HasDatabaseName("vehicle_types_code_key");
                entity.HasIndex(e => e.Slug).IsUnique().HasDatabaseName("vehicle_types_slug_key");
                entity.HasIndex(e => e.CategoryId).HasDatabaseName("idx_vehicle_types_category");
                entity.HasIndex(e => e.IsActive).HasDatabaseName("idx_vehicle_types_is_active");

                entity.Property(e => e.Metadata).HasColumnType("jsonb");

                entity.HasOne(e => e.Category)
                      .WithMany()
                      .HasForeignKey(e => e.CategoryId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("fk_vehicle_types_category");
            });

            // ─── Location indexes ─────────────────────────────────────────────
            modelBuilder.Entity<Location>()
                .HasIndex(l => new { l.CountryCode, l.StateCode, l.CityName })
                .HasDatabaseName("IX_Location_Country_State_City");

            modelBuilder.Entity<Location>()
                .HasIndex(l => l.Pincode)
                .HasDatabaseName("IX_Location_Pincode");

            // ─── CityArea indexes ─────────────────────────────────────────────
            modelBuilder.Entity<CityArea>()
                .HasIndex(ca => ca.Pincode)
                .HasDatabaseName("IX_CityArea_Pincode");
        }
    }
}