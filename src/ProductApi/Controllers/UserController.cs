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
        public async Task<IActionResult> Create(UserCreateDto userDto, CancellationToken cancellationToken)
        {
            return Ok(await _createUserUseCase.Execute(userDto, cancellationToken));
        }

        [HttpGet]
        public async Task<IActionResult> ReadAll(CancellationToken cancellationToken)
        {
            return Ok(await _getUsersUseCase.Execute(cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Read(int id, CancellationToken cancellationToken)
        {
            return Ok(await _getUserUseCase.Execute(id, cancellationToken));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserUpdateDto userUpdateDto, CancellationToken cancellationToken)
        {
            return Ok(await _updateUserUseCase.Execute(id, userUpdateDto, cancellationToken));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            return Ok(await _deleteUserUseCase.Execute(id, cancellationToken));
        }

    }
}
