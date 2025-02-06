using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProductApi.Application.DTOs;
using ProductApi.Application.Models;
using ProductApi.Domain.Entities;
using ProductApi.Domain.Exceptions;
using ProductApi.Domain.Interfaces;

namespace ProductApi.Application.UseCases
{
    public class UpdateProductUseCase(IProductRepository repository, IMapper mapper)
    {
        private readonly IProductRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        public async Task Execute(Product productUpdate)
        {
            var product = await _repository.GetByIdAsync(productUpdate.Id) ?? throw new NotFoundException($"Product with ID {productUpdate.Id} not found.");

            product.Name = productUpdate.Name;
            product.Price = productUpdate.Price;

            await _repository.UpdateAsync(product);
        }
    }
}
