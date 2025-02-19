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
    public class UserController(CreateUserUseCase createUserUseCase, GetUsersUseCase getUsersUseCase, GetUserUseCase getUserUseCase, UpdateUserUseCase updateUserUseCase, DeleteUserUseCase deleteUserUseCase) : ControllerBase 
    {
        private readonly CreateUserUseCase _createUserUseCase = createUserUseCase;
        private readonly GetUsersUseCase _getUsersUseCase = getUsersUseCase;
        private readonly GetUserUseCase _getUserUseCase = getUserUseCase;
        private readonly UpdateUserUseCase _updateUserUseCase = updateUserUseCase;
        private readonly DeleteUserUseCase _deleteUserUseCase = deleteUserUseCase;

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateDto userDto)
        {
            return Ok(await _createUserUseCase.Execute(userDto));
        }

        [HttpGet]
        public async Task<IActionResult> ReadAll()
        {
            return Ok(await _getUsersUseCase.Execute());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Read(int id)
        {
            return Ok(await _getUserUseCase.Execute(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserUpdateDto userUpdateDto)
        {
            return Ok(await _updateUserUseCase.Execute(id, userUpdateDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _deleteUserUseCase.Execute(id));
        }

    }
}
