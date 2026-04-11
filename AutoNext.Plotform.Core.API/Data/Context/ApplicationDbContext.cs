using AutoNext.Plotform.Core.API.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AutoNext.Plotform.Core.API.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

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

            // Composite indexes for faster search
            modelBuilder.Entity<Location>()
                .HasIndex(l => new { l.CountryCode, l.StateCode, l.CityName })
                .HasDatabaseName("IX_Location_Country_State_City");

            modelBuilder.Entity<Location>()
                .HasIndex(l => l.Pincode)
                .HasDatabaseName("IX_Location_Pincode");

            modelBuilder.Entity<CityArea>()
                .HasIndex(ca => ca.Pincode)
                .HasDatabaseName("IX_CityArea_Pincode");

            // Seed initial India data
            //SeedIndiaLocations(modelBuilder);
        }
    }
}
