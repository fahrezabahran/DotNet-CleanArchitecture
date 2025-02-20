using Microsoft.EntityFrameworkCore;
using ProductApi.Infrastructure.Persistence; // Import namespace untuk ApplicationDbContext
using ProductApi.Infrastructure.Repositories; // Import namespace untuk repositori
using ProductApi.Infrastructure.Configurations; // Import namespace untuk konfigurasi layanan
using ProductApi.Application.Interfaces; // Import namespace untuk antarmuka repositori
using DotNet_CleanArchitecture.Middleware;
using ProductApi.Application;
using ProductApi.Application.UseCases.ProductUseCase;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices(builder.Configuration); // Konfigurasi DbContext
builder.Services.AddApplicationServices(); // Use Cases
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly); // Auto Mapper

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