using Microsoft.EntityFrameworkCore;
using ProductApi.Infrastructure.Persistence; // Import namespace untuk ApplicationDbContext
using ProductApi.Infrastructure.Repositories; // Import namespace untuk repositori
using ProductApi.Domain.Interfaces; // Import namespace untuk antarmuka repositori
using ProductApi.Application.UseCases;
using DotNet_CleanArchitecture.Middleware;
using ProductApi.Application; // Import namespace untuk use cases

var builder = WebApplication.CreateBuilder(args);

// Konfigurasi DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Konfigurasi Dependency Injection
builder.Services.AddScoped<IProductRepository, ProductRepository>(); // Repositori


// Use Case
builder.Services.AddScoped<CreateProductUseCase>();
builder.Services.AddScoped<GetProductUseCase>();
builder.Services.AddScoped<GetAllProductsUseCase>();
builder.Services.AddScoped<UpdateProductUseCase>();
builder.Services.AddScoped<DeleteProductUseCase>();

// Auto Mapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// Konfigurasi Controller dan Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Konfigurasi Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
//app.UseMiddleware<ExceptionHandlingMiddleware>();
//app.UseMiddleware<RequestResponseLoggingMiddleware>();

app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<AuthenticationMiddleware>();
app.UseMiddleware<AuthorizationMiddleware>();
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RoutingMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();