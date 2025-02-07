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

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserDto userDto)
        {
            await _loginUseCase.Execute(userDto);

            // Validasi kredensial pengguna
            if (userDto.UserName == "test" && userDto.Password == "password") // Ganti dengan logika validasi Anda
            {
                var token = GenerateJwtToken(userDto.UserName);
                var refreshToken = GenerateRefreshToken(); // Menghasilkan refresh token
                return Ok(new { Token = token, RefreshToken = refreshToken });
            }

            return Unauthorized();
        }

        [HttpPost("refresh")]
        public IActionResult Refresh([FromBody] RefreshTokenRequest request)
        {
            // Validasi refresh token (implementasikan logika Anda)
            if (IsValidRefreshToken(request.RefreshToken)) // Ganti dengan logika validasi Anda
            {
                var newToken = GenerateJwtToken(request.Username);
                var newRefreshToken = GenerateRefreshToken(); // Menghasilkan refresh token baru
                return Ok(new { Token = newToken, RefreshToken = newRefreshToken });
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

        private string GenerateRefreshToken()
        {
            // Logika untuk menghasilkan refresh token
            // Anda bisa menggunakan GUID atau metode lain untuk menghasilkan token yang unik
            return Guid.NewGuid().ToString();
        }

        private bool IsValidRefreshToken(string refreshToken)
        {
            // Implementasikan logika untuk memvalidasi refresh token
            // Misalnya, periksa apakah refresh token ada dalam database atau cache
            return true; // Placeholder
        }

        public class RefreshTokenRequest
        {
            public string Username { get; set; }
            public string RefreshToken { get; set; }
        }
    }
}
