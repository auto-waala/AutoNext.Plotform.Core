using AutoNext.Plotform.Core.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

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
