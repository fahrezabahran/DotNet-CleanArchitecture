using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProductApi.Application.DTOs;
using ProductApi.Application.Models;
using ProductApi.Domain.Entities;
using ProductApi.Domain.Interfaces;

namespace ProductApi.Application.UseCases.ProductUseCase
{
    public class GetProductUseCase(IProductRepository productRepository, IMapper mapper)
    {
        private readonly IProductRepository _repository = productRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<Product?> Execute(int id)
        {
            var product = await _repository.GetByIdAsync(id);

            return product;
        }
    }
}
