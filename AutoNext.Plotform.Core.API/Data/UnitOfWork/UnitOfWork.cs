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
