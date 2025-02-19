using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductApi.Application.Responses;
using ProductApi.Domain.Entities;

namespace ProductApi.Application.Interfaces
{
    public interface IUpdateProductUseCase
    {
        Task<BaseResponse> Execute(int id, Product product);
    }
}
