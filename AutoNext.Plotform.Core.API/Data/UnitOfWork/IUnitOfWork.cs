using AutoNext.Plotform.Core.API.Data.Repositories;
using AutoNext.Plotform.Core.API.Models.Entities;

namespace AutoNext.Plotform.Core.API.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Location> Locations { get; }
        IRepository<CityArea> CityAreas { get; }
        IRepository<VehicleType> VehicleTypes { get; }
        IRepository<FuelType> FuelTypes { get; }
        IRepository<Transmission> Transmissions { get; }
        IRepository<Brand> Brands { get; }
        IRepository<Category> Categories { get; }
        IRepository<Color> Colors { get; }
        IRepository<DocumentType> DocumentTypes { get; }
        IRepository<Feature> Features { get; }
        IRepository<InspectionChecklist> InspectionChecklists { get; }
        IRepository<PaymentMethod> PaymentMethods { get; }
        IRepository<VehicleModel> VehicleModels { get; }
        IRepository<ServiceType> ServiceTypes { get; }
        IRepository<ShippingOption> ShippingOptions { get; }
        IRepository<TaxRate> TaxRates { get; }
        IRepository<TitleType> TitleTypes { get; }
        IRepository<VehicleVariant> VehicleVariants { get; }
        IRepository<VehicleCondition> VehicleConditions { get; }
        IRepository<WarrantyType> WarrantyTypes { get; }

        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
