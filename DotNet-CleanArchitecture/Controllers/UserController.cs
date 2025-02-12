using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Application.DTOs;
using ProductApi.Application.Responses;
using ProductApi.Application.UseCases.UserUseCase;

namespace DotNet_CleanArchitecture.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(CreateUserUseCase createUserUseCase, GetUsersUseCase getUsersUseCase) : ControllerBase 
    {
        private readonly CreateUserUseCase _createUserUseCase = createUserUseCase;
        private readonly GetUsersUseCase _getUsersUseCase = getUsersUseCase;

        [HttpPost]
        public async Task<IActionResult> Post(UserCreateDto userDto)
        {
            return Ok(await _createUserUseCase.Execute(userDto));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _getUsersUseCase.Execute());
        }

    }
}
