﻿using System;
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
using ProductApi.Application.Interfaces;
using ProductApi.Application.Responses;

namespace ProductApi.Application.UseCases.ProductUseCase
{
    public class UpdateProductUseCase(IProductRepository repository, IMapper mapper)
    {
        private readonly IProductRepository _repository = repository;

        public async Task<BaseResponse> Execute(int id, Product productUpdate)
        {
            if (!id.Equals(productUpdate.Id))
                return new ErrorResponse("Product Id Miss Match");

            var product = await _repository.GetByIdAsync(productUpdate.Id);

            if (product == null)
                return new ErrorResponse("Product Not Found");

            product.Name = productUpdate.Name;
            product.Price = productUpdate.Price;

            await _repository.UpdateAsync(product);

            return new SuccessResponse<Product>(product);
        }
    }
}
