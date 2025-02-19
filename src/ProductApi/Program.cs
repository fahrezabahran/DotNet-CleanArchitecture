using Microsoft.EntityFrameworkCore;
using ProductApi.Infrastructure.Persistence; // Import namespace untuk ApplicationDbContext
using ProductApi.Infrastructure.Repositories; // Import namespace untuk repositori
using ProductApi.Application.Interfaces; // Import namespace untuk antarmuka repositori
using DotNet_CleanArchitecture.Middleware;
using ProductApi.Application;
using ProductApi.Application.UseCases.ProductUseCase;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ProductApi.Application.UseCases.Login;
using StackExchange.Redis;
using ProductApi.Application.UseCases.UserUseCase; // Import namespace untuk use cases

var builder = WebApplication.CreateBuilder(args);

// Konfigurasi DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost")); // Ganti dengan string koneksi Redis Anda


// Konfigurasi Dependency Injection
builder.Services.AddScoped<IProductRepository, ProductRepository>(); // Repositori
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


// Use Case
builder.Services.AddScoped<CreateProductUseCase>();
builder.Services.AddScoped<GetProductUseCase>();
builder.Services.AddScoped<GetAllProductsUseCase>();
builder.Services.AddScoped<UpdateProductUseCase>();
builder.Services.AddScoped<DeleteProductUseCase>();

builder.Services.AddScoped<CreateUserUseCase>();
builder.Services.AddScoped<GetUsersUseCase>();
builder.Services.AddScoped<GetUserUseCase>();
builder.Services.AddScoped<UpdateUserUseCase>();
builder.Services.AddScoped<DeleteUserUseCase>();

builder.Services.AddScoped<LoginUseCase>();

// Auto Mapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// Konfigurasi Controller dan Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure JWT authentication
var key = builder.Configuration["Jwt:Key"];
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
    };
});

var app = builder.Build();

// Konfigurasi Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
app.UseMiddleware<ExceptionHandlingMiddleware>();

//app.UseMiddleware<RequestResponseLoggingMiddleware>();
//app.UseMiddleware<LoggingMiddleware>();
//app.UseMiddleware<AuthenticationMiddleware>();
//app.UseMiddleware<AuthorizationMiddleware>();
//app.UseMiddleware<ErrorHandlingMiddleware>();
//app.UseMiddleware<RoutingMiddleware>();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();