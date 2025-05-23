﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProductApi.Application.Dtos;
using ProductApi.Application.Responses;
using ProductApi.Application.Services;
using ProductApi.Application.UseCases.Login;

namespace DotNet_CleanArchitecture.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(LoginUseCase loginUseCase, ILoginService loginService) : ControllerBase
    {
        private readonly LoginUseCase _loginUseCase = loginUseCase;
        private readonly ILoginService _loginService = loginService;

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserCreateDto userCreateDto, CancellationToken cancellationToken)
        {
            return Ok(await _loginService.Login(userCreateDto, cancellationToken));
        }

        //[HttpPost("refresh")]
        //public IActionResult Refresh([FromBody] RefreshTokenRequest request)
        //{
        //    // Validasi refresh token (implementasikan logika Anda)
        //    if (IsValidRefreshToken(request.RefreshToken)) // Ganti dengan logika validasi Anda
        //    {
        //        var newToken = GenerateJwtToken(request.Username);
        //        var newRefreshToken = GenerateRefreshToken(); // Menghasilkan refresh token baru
        //        return Ok(new { Token = newToken, RefreshToken = newRefreshToken });
        //    }

        //    return Unauthorized();
        //}

       

        
    }
}
