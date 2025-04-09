using ProductApi.Domain.Interfaces;
using ProductApi.Infrastructure.Persistence.Repositories;
using ProductApi.Domain.Entities;
using ProductApi.Application.Interfaces;
using ProductApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Identity.Client;

namespace ProductApi.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork, IDisposable, IAsyncDisposable
    {
        private bool _disposed;
        public IBaseRepository<User> Users { get; }
        public IBaseRepository<UserActivity> UserActivities { get; }

        private readonly ApplicationDbContext _context;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Users = new BaseRepository<User>(_context);
            UserActivities = new BaseRepository<UserActivity>(_context);
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction != null)
                throw new InvalidOperationException("Transaction already started.");

            _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction == null) throw new InvalidOperationException("No transaction started.");
            await _transaction.CommitAsync(cancellationToken);
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction == null) throw new InvalidOperationException("No transaction started.");
            await _transaction.RollbackAsync(cancellationToken);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task CompleteAsync(CancellationToken cancellationToken = default)
        {
            await SaveChangesAsync(cancellationToken);
            await CommitAsync(cancellationToken);
        }

        public void Dispose()
        {
            if (_disposed) return;
            _context.Dispose();
            _transaction?.Dispose();
            _disposed = true;
        }

        public async ValueTask DisposeAsync()
        {
            if (_disposed) return;
            await _context.DisposeAsync();
            if (_transaction != null)
                await _transaction.DisposeAsync();
            _disposed = true;
        }
    }
}
