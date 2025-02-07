using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProductApi.Application.DTOs;
using ProductApi.Application.UseCases.Login;

namespace DotNet_CleanArchitecture.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly LoginUseCase _loginUseCase;
        private readonly IConfiguration _configuration;

        public AuthController(LoginUseCase loginUseCase, IConfiguration configuration)
        {
            _loginUseCase = loginUseCase;
            _configuration = configuration;
        }

        [HttpPost("login")] // Pastikan menggunakan huruf kecil untuk konsistensi
        public async Task<ActionResult> Login([FromBody] UserDto userDto)
        {
            await _loginUseCase.Execute(userDto);

            // Validasi kredensial pengguna
            if (userDto.UserName == "test" && userDto.Password == "password") // Ganti dengan logika validasi Anda
            {
                var token = GenerateJwtToken(userDto.UserName);
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }

        [HttpPost("refresh")] // Pastikan menggunakan huruf kecil untuk konsistensi
        public IActionResult Refresh([FromBody] RefreshTokenRequest request)
        {
            // Validasi token refresh (implementasikan logika Anda)
            if (IsValidRefreshToken(request.RefreshToken)) // Ganti dengan logika validasi Anda
            {
                var newToken = GenerateJwtToken(request.Username);
                return Ok(new { Token = newToken });
            }

            return Unauthorized();
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

        private bool IsValidRefreshToken(string refreshToken)
        {
            // Implementasikan logika untuk memvalidasi token refresh
            return true; // Placeholder
        }

        public class RefreshTokenRequest
        {
            public string Username { get; set; }
            public string RefreshToken { get; set; }
        }
    }
}
