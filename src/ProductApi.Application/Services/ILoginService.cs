using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductApi.Application.Dtos;
using ProductApi.Application.Responses;

namespace ProductApi.Application.Services
{
    public interface ILoginService
    {
        Task<ApiResponse<object>> Login(UserCreateDto userCreateDto, CancellationToken cancellationToken);
    }
}
