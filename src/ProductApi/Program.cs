using DotNet_CleanArchitecture.Middlewares;
using ProductApi.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ProductApi.Infrastructure.DependencyInjection;
using ProductApi.Application.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)   // Default config
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)           // Environment-specific config
    .AddEnvironmentVariables();                                               // Docker / System Environment

builder.Services
    .AddInfrastructureServices(builder.Configuration)
    .AddApplicationServices()
    .AddAutoMapper(typeof(MappingProfile).Assembly);

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