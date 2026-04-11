using AutoNext.Plotform.Core.API.Data.Context;
using AutoNext.Plotform.Core.API.Data.Repositories;
using AutoNext.Plotform.Core.API.Models.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace AutoNext.Plotform.Core.API.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction? _transaction;
        private bool _disposed;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        private IRepository<Location>? _locations;
        private IRepository<CityArea>? _cityAreas;
        private IRepository<VehicleType>? _vehicleTypes;
        private IRepository<FuelType>? _fuelTypes;
        private IRepository<Transmission>? _transmissions;
        private IRepository<Brand>? _brands;
        private IRepository<Category>? _categories;
        private IRepository<Color>? _colors;
        private IRepository<DocumentType>? _documentTypes;
        private IRepository<Feature>? _features;
        private IRepository<InspectionChecklist>? _InspectionChecklists;
        private IRepository<PaymentMethod>? _paymentMethods;
        private IRepository<VehicleModel>? _vehicleModels;
        private IRepository<ServiceType>? _serviceTypes;
        private IRepository<ShippingOption>? _shippingOptions;
        private IRepository<TaxRate>? _taxRates;
        private IRepository<TitleType>? _titleTypes;
        private IRepository<VehicleVariant>? _vehicleVariants;
        private IRepository<VehicleCondition> _vehicleConditions;
        private IRepository<WarrantyType>? _warrantyTypes;

        public IRepository<Feature> Features =>
            _features ??= new Repository<Feature>(_context);
        public IRepository<Location> Locations =>
            _locations ??= new Repository<Location>(_context);

        public IRepository<CityArea> CityAreas =>
            _cityAreas ??= new Repository<CityArea>(_context);

        public IRepository<VehicleType> VehicleTypes =>
          _vehicleTypes ??= new Repository<VehicleType>(_context);

        public IRepository<FuelType> FuelTypes =>
            _fuelTypes ??= new Repository<FuelType>(_context);

        public IRepository<Transmission> Transmissions =>
            _transmissions ??= new Repository<Transmission>(_context);

        public IRepository<Brand> Brands =>
            _brands ??= new Repository<Brand>(_context);

        public IRepository<Category> Categories =>
            _categories ??= new Repository<Category>(_context);

        public IRepository<Color> Colors =>
            _colors ??= new Repository<Color>(_context);

        public IRepository<DocumentType> DocumentTypes =>
            _documentTypes ??= new Repository<DocumentType>(_context);

        public IRepository<InspectionChecklist> InspectionChecklists =>
            _InspectionChecklists ??= new Repository<InspectionChecklist>(_context);

        public IRepository<PaymentMethod> PaymentMethods =>
             _paymentMethods ??= new Repository<PaymentMethod>(_context);

        public IRepository<VehicleModel> VehicleModels =>
             _vehicleModels ??= new Repository<VehicleModel>(_context);

        public IRepository<ServiceType> ServiceTypes =>
             _serviceTypes ??= new Repository<ServiceType>(_context);

        public IRepository<ShippingOption> ShippingOptions =>
            _shippingOptions ??= new Repository<ShippingOption>(_context);

        public IRepository<TaxRate> TaxRates =>
            _taxRates ??= new Repository<TaxRate>(_context);

        public IRepository<TitleType> TitleTypes =>
            _titleTypes ??= new Repository<TitleType>(_context);

        public IRepository<VehicleVariant> VehicleVariants =>
            _vehicleVariants ??= new Repository<VehicleVariant>(_context);

        public IRepository<VehicleCondition> VehicleConditions =>
            _vehicleConditions ??= new Repository<VehicleCondition>(_context);

        public IRepository<WarrantyType> WarrantyTypes =>
            _warrantyTypes ??= new Repository<WarrantyType>(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
                await _transaction.CommitAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
                await _transaction.RollbackAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
                _transaction?.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
