using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductApi.Domain.Entities;

namespace ProductApi.Application.Interfaces.ProductInterfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellation);
        Task<Product?> GetByIdAsync(int id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
    }
}
