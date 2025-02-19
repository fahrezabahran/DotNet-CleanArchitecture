using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductApi.Domain.Exceptions;
using ProductApi.Application.Interfaces;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using ProductApi.Application.Responses;
using ProductApi.Domain.Entities;

namespace ProductApi.Application.UseCases.ProductUseCase
{
    public class DeleteProductUseCase(IProductRepository productRepository) : IDeleteProductUseCase
    {
        private readonly IProductRepository _productRepository = productRepository;

        public async Task<BaseResponse> Execute(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
                return new ErrorResponse("Product Not Found !");

            await _productRepository.DeleteAsync(id);

            return new SuccessResponse<Product>(product);
        }
    }
}
