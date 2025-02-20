using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductApi.Application.Interfaces;
using ProductApi.Application.Responses;
using ProductApi.Application.UseCases.Token;
using ProductApi.Domain.Entities;

namespace ProductApi.Application.UseCases.UserUseCase
{
    public class DeleteUserUseCase(IGenericRepository<User> userRepository)
    {
        private readonly IGenericRepository<User> _userRepository = userRepository;

        public async Task<BaseResponse> Execute(int id, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(id, cancellationToken);

            if (user == null)
                return new ErrorResponse("User Not Found");

            await _userRepository.DeleteAsync(user.UserId, cancellationToken);

            return new SuccessResponse<object>(new { });
        }
    }
}
