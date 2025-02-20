using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductApi.Application.Interfaces.ProductInterfaces;
using ProductApi.Application.Interfaces;
using ProductApi.Infrastructure.Persistence;
using ProductApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ProductApi.Infrastructure.Configurations
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Database
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                sqlOptions => { sqlOptions.CommandTimeout(60).MaxBatchSize(1000); }
                ));

            // Repository
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }

}