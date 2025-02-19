﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductApi.Application.Responses;

namespace ProductApi.Application.Interfaces
{
    public interface IGetProductUseCase
    {
        Task<BaseResponse> Execute(int id);
    }
}
