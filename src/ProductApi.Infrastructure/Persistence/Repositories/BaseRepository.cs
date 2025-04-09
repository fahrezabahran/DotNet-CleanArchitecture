using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProductApi.Application.Interfaces;

namespace ProductApi.Infrastructure.Persistence.Repositories
{
    // Data/Repository.cs
    public class BaseRepository<T>(ApplicationDbContext context) : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context = context;
        protected readonly DbSet<T> _dbSet = context.Set<T>();

        public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken) => await _dbSet.FindAsync(id, cancellationToken);
        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken) => await _dbSet.ToListAsync(cancellationToken);
        public async Task AddAsync(T entity, CancellationToken cancellationToken) => await _dbSet.AddAsync(entity, cancellationToken);
        public void Update(T entity) => _dbSet.Update(entity);
        public void Remove(T entity) => _dbSet.Remove(entity);
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbSet.Where(predicate).ToListAsync(cancellationToken);
        }
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _dbSet.AnyAsync(predicate, cancellationToken);
        }
    }
}
