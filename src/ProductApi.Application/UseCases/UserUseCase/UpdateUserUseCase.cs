using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProductApi.Application.DTOs;
using ProductApi.Application.Interfaces;
using ProductApi.Application.Responses;
using ProductApi.Domain.Entities;

namespace ProductApi.Application.UseCases.UserUseCase
{
    public class UpdateUserUseCase(IGenericRepository<User> userRepository, IMapper mapper)
    {
        private readonly IGenericRepository<User> _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse> Execute(int id, UserUpdateDto userUpdateDto, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(id, cancellationToken);

            if (user == null)
                return new ErrorResponse("User Not Found");

            var userUpdate = _mapper.Map<User>(userUpdateDto);

            await _userRepository.UpdateAsync(userUpdate, cancellationToken);

            var userDto = _mapper.Map<UserDto>(userUpdateDto);

            return new SuccessResponse<UserDto>(userDto);
        }
    }
}
