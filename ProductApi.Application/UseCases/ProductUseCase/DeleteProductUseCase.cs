using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductApi.Domain.Exceptions;
using ProductApi.Domain.Interfaces;

namespace ProductApi.Application.UseCases.ProductUseCase
{
    public class DeleteProductUseCase(IProductRepository productRepository)
    {
        private readonly IProductRepository _productRepository = productRepository;

        public async Task Execute(int id)
        {
            _ = await _productRepository.GetByIdAsync(id) ?? throw new NotFoundException($"Product with ID {id} not found.");

            await _productRepository.DeleteAsync(id);
        }
    }
}
