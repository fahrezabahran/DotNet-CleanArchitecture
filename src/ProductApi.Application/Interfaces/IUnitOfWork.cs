using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductApi.Domain.Entities;
using ProductApi.Domain.Interfaces;

namespace ProductApi.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IBaseRepository<User> Users { get; }
        IBaseRepository<UserActivity> UserActivities { get; }
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitAsync(CancellationToken cancellationToken = default);
        Task RollbackAsync(CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task CompleteAsync(CancellationToken cancellationToken = default);
        void Dispose();
        ValueTask DisposeAsync();
    }
}
