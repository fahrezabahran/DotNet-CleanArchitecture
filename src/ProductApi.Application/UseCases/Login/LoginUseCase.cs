using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductApi.Application.DTOs;
using ProductApi.Domain.Entities;
using ProductApi.Application.Interfaces;
using BCrypt.Net;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using ProductApi.Application.Responses;
using System.Transactions;

namespace ProductApi.Application.UseCases.Login
{
    public class LoginUseCase(IGenericRepository<User> userRepository, IGenericRepository<UserActivity> userActivityRepository, IConfiguration configuration)
    {
        private readonly IGenericRepository<User> _userRepository = userRepository;
        private readonly IGenericRepository<UserActivity> _userActivityRepository = userActivityRepository;
        private readonly IConfiguration _configuration = configuration;
        public async Task<BaseResponse> Execute(UserCreateDto userCreateDto, CancellationToken cancellationToken)
        {
            var users = await _userRepository.FindAsync(u => u.UserName.Contains(userCreateDto.UserName), cancellationToken);

            var user = users.SingleOrDefault();

            if (user == null)
                return new ErrorResponse("User Not Found");

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {

                var userActivity = new UserActivity() { UserId = user.UserId, Login = true };
                await _userActivityRepository.AddAsync(userActivity, cancellationToken);

                if (!BCrypt.Net.BCrypt.Verify(userCreateDto.Password, user.Password))
                {
                    user.FalsePwdCount += 1;
                    await _userRepository.UpdateAsync(user, cancellationToken);
                    return new ErrorResponse("Wrong Password");
                }

                var token = GenerateJwtToken(userCreateDto.UserName);
                var refreshToken = GenerateRefreshToken();

                scope.Complete();

                return new SuccessResponse<object>(new { Token = token, RefreshToken = refreshToken }, "Successfull");
            }
        }

        private string GenerateJwtToken(string username)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30), // Waktu kedaluwarsa token
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }

        private bool IsValidRefreshToken(string refreshToken)
        {
            // Implementasikan logika untuk memvalidasi refresh token
            // Misalnya, periksa apakah refresh token ada dalam database atau cache
            return true; // Placeholder
        }
    }
}
