using AutoNext.Plotform.Core.API.Data.Repositories;
using AutoNext.Plotform.Core.API.Models.Entities;

namespace AutoNext.Plotform.Core.API.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Location> Locations { get; }
        IRepository<CityArea> CityAreas { get; }

        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
