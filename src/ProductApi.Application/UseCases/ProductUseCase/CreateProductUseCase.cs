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
    public class CreateProductUseCase(IProductRepository productRepository, IMapper mapper) : ICreateProductUseCase
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse> Execute(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            await _productRepository.AddAsync(product);

            return new SuccessResponse<Product>(product);
        }
    }
}
