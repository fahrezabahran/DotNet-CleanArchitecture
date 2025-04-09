using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProductApi.Application.Dtos;
using ProductApi.Application.Models;
using ProductApi.Domain.Entities;
using ProductApi.Application.Responses;
using ProductApi.Application.Interfaces.ProductInterfaces;

namespace ProductApi.Application.UseCases.ProductUseCase
{
    public class GetProductUseCase(IProductRepository productRepository) : IGetProductUseCase
    {
        private readonly IProductRepository _repository = productRepository;

        public async Task<BaseResponse> Execute(int id)
        {
            var product = await _repository.GetByIdAsync(id);

            return new SuccessResponse<Product?>(product);  
        }
    }
}
