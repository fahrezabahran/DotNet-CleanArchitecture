using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ProductApi.Application.Interfaces.ProductInterfaces;
using ProductApi.Application.Interfaces.Timesheet;
using ProductApi.Application.Interfaces;
using ProductApi.Application.Services;
using ProductApi.Application.UseCases.ProductUseCase;
using ProductApi.Application.UseCases.Timesheet;
using ProductApi.Application.UseCases.UserUseCase;

namespace ProductApi.Application.DependencyInjection
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICreateProductUseCase, CreateProductUseCase>();
            services.AddScoped<IGetProductUseCase, GetProductUseCase>();
            services.AddScoped<IGetAllProductsUseCase, GetAllProductsUseCase>();
            services.AddScoped<IUpdateProductUseCase, UpdateProductUseCase>();
            services.AddScoped<IDeleteProductUseCase, DeleteProductUseCase>();

            services.AddScoped<ICreateTimesheetUseCase, CreateTimesheetUseCase>();

            services.AddScoped<CreateUserUseCase>();
            services.AddScoped<GetUsersUseCase>();
            services.AddScoped<GetUserUseCase>();
            services.AddScoped<UpdateUserUseCase>();
            services.AddScoped<DeleteUserUseCase>();


            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ILoginService, LoginService>();

            return services;
        }
    }
}
