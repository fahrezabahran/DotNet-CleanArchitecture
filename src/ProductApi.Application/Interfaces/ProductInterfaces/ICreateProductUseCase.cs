using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductApi.Application.DTOs;
using ProductApi.Application.Responses;
using ProductApi.Domain.Entities;

namespace ProductApi.Application.Interfaces.ProductInterfaces
{
    public interface ICreateProductUseCase
    {
        Task<BaseResponse> Execute(ProductDto productDto);
    }   
}
