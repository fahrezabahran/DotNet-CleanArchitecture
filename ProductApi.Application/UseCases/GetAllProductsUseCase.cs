using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductApi.Application.DTOs;
using ProductApi.Domain.Entities;
using ProductApi.Domain.Interfaces;

namespace ProductApi.Application.UseCases
{
    public class GetAllProductsUseCase
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsUseCase(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>?> Execute()
        {
            var products = await _productRepository.GetAllAsync();
            
            return products;
        }
    }
}
