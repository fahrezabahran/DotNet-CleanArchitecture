using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductApi.Application.DTOs;
using ProductApi.Domain.Entities;
using ProductApi.Application.Interfaces;
using ProductApi.Application.Responses;

namespace ProductApi.Application.UseCases.ProductUseCase
{
    public class GetAllProductsUseCase(IProductRepository productRepository) : IGetAllProductsUseCase
    {
        private readonly IProductRepository _productRepository = productRepository;

        public async Task<BaseResponse> Execute(CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync(cancellationToken);

            return new SuccessResponse<IEnumerable<Product>>(products);
        }
    }
}
