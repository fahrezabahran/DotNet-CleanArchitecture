using Microsoft.Extensions.DependencyInjection;
using ProductApi.Application.Interfaces.ProductInterfaces;
using ProductApi.Application.Interfaces.Timesheet;
using ProductApi.Application.UseCases.Login;
using ProductApi.Application.UseCases.ProductUseCase;
using ProductApi.Application.UseCases.Timesheet;
using ProductApi.Application.UseCases.UserUseCase;

namespace ProductApi.Infrastructure.Configurations
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

            services.AddScoped<LoginUseCase>();

            return services;
        }
    }

}
