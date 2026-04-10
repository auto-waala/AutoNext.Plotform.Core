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

        public IRepository<Location> Locations =>
            _locations ??= new Repository<Location>(_context);

        public IRepository<CityArea> CityAreas =>
            _cityAreas ??= new Repository<CityArea>(_context);

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
