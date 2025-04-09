using ProductApi.Domain.Entities;

namespace ProductApi.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> GetByIdAsync(int id);
        Task AddAsync(Order order);
        Task<IEnumerable<Order>> GetAllAsync();
        Task UpdateAsync(Order order);
    }
}
