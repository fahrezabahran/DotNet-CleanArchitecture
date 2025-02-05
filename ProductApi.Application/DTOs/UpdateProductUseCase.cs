using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProductApi.Application.Models;
using ProductApi.Domain.Entities;
using ProductApi.Domain.Interfaces;

namespace ProductApi.Application.DTOs
{
    public class UpdateProductUseCase(IProductRepository repository, IMapper mapper)
    {
        private readonly IProductRepository _repository = repository;
        private readonly IMapper _mapper = mapper;  

        public async Task<ApiResponse<ProductDto>> Execute(ProductDto productDto)
        {
            var product = await _repository.GetByIdAsync(productDto.Id);

            if (product == null)
            {
                return new ApiResponse<ProductDto>() { Success = false, Message = "Not Found" };
            }

            product.Name = productDto.Name;
            product.Price = productDto.Price;

            await _repository.UpdateAsync(product);

            return new ApiResponse<ProductDto>()
            {
                Success = true,
                Message = "Successfull",
                Data = new ProductDto { Id = product.Id, Name = product.Name, Price = product.Price }
            };
        }
    }
}
