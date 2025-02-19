using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProductApi.Application.DTOs;
using ProductApi.Application.Models;
using ProductApi.Domain.Entities;
using ProductApi.Application.Interfaces;
using ProductApi.Application.Responses;

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
