using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using ProductApi.Application.Dtos;
using ProductApi.Application.Interfaces;
using ProductApi.Application.Responses;
using ProductApi.Domain.Entities;
namespace ProductApi.Application.Services
{
    class LoginService(IUnitOfWork unitOfWork, IConfiguration configuration) : ILoginService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IConfiguration _configuration = configuration;

        public async Task<ApiResponse<object>> Login(UserCreateDto userCreateDto, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync(cancellationToken);
                var user = (await _unitOfWork.Users.FindAsync(u => u.UserName.Equals(userCreateDto.UserName), cancellationToken)).SingleOrDefault();

                if (user == null || !user.IsActive)
                    return ApiResponse<object>.FailResponse("login failed", "User Not Found");

                if (user.IsRevoke)
                    return ApiResponse<object>.FailResponse("login failed", "User Locked");

                if (!BCrypt.Net.BCrypt.Verify(userCreateDto.Password, user.Password))
                {
                    user.FalsePwdCount += 1;

                    if (user.FalsePwdCount.Equals(3))
                    {
                        user.IsRevoke = true;
                    }

                    _unitOfWork.Users.Update(user);
                    await _unitOfWork.CompleteAsync(cancellationToken);
                    return ApiResponse<object>.FailResponse("login failed", "Wrong Password");
                }

                if (user.IsLogin)
                    return ApiResponse<object>.FailResponse("login failed", "User Current Loggin");

                // reset count if login success
                if (user.FalsePwdCount > 0)
                {
                    user.FalsePwdCount = 0;
                    _unitOfWork.Users.Update(user);
                }

                user.IsLogin = true;

                var token = GenerateJwtToken(userCreateDto.UserName);
                var refreshToken = GenerateRefreshToken();

                var userActivity = new UserActivity() { UserId = user.UserId, Login = true, ActivityDate = DateTime.Now };
                await _unitOfWork.UserActivities.AddAsync(userActivity, cancellationToken);
                await _unitOfWork.CompleteAsync(cancellationToken);
                return ApiResponse<object>.SuccessResponse(new { Token = token, RefreshToken = refreshToken });
            }
            catch
            {
                await _unitOfWork.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<BaseResponse> Logout(UserCreateDto userCreateDto)
        {
            var user = (await _unitOfWork.Users.FindAsync(u => u.UserName.Equals(userCreateDto.UserName))).SingleOrDefault();

            if(user != null)
            {
                user.IsLogin = false;
            }

            return new SuccessResponse<object>(true, "Successfull");
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
    }
}
