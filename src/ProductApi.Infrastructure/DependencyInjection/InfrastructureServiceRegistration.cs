using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductApi.Application.Interfaces.ProductInterfaces;
using ProductApi.Application.Interfaces;
using ProductApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using ProductApi.Domain.Interfaces;
using ProductApi.Infrastructure.Persistence.Repositories;
using ProductApi.Infrastructure.Repositories;

namespace ProductApi.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                sqlOptions => { 
                    sqlOptions
                    .CommandTimeout(60)
                    .MaxBatchSize(1000)
                    //.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null)
                    ; }
                ));

            // Repository
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }

}