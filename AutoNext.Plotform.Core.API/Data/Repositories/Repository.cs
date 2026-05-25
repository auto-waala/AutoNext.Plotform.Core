using AutoNext.Plotform.Core.API.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AutoNext.Plotform.Core.API.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        // ─── Base query — AsNoTracking + IgnoreAutoIncludes on every read ─────
        protected IQueryable<T> ReadQuery => _dbSet.AsNoTracking().IgnoreAutoIncludes();

        public async Task<T?> GetByIdAsync(Guid id)
        {
            // FindAsync bypasses the query pipeline — use FirstOrDefaultAsync instead
            return await ReadQuery.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await ReadQuery.ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await ReadQuery.Where(predicate).ToListAsync();
        }

        public async Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await ReadQuery.SingleOrDefaultAsync(predicate);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
        {
            return predicate == null
                ? await ReadQuery.CountAsync()
                : await ReadQuery.CountAsync(predicate);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await ReadQuery.AnyAsync(predicate);
        }
    }
}