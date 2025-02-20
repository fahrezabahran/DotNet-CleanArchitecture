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
    public class CreateUserUseCase(IGenericRepository<User> userRepository, IMapper mapper)
    {
        private readonly IGenericRepository<User> _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse> Execute(UserCreateDto userCreateDto, CancellationToken cancellationToken)
        {
            var userExist = await _userRepository.FindAsync(u => u.UserName == userCreateDto.UserName.Trim(), cancellationToken);

            if (userExist.Any())
                return new ErrorResponse("Username already exists");

            userCreateDto.Password = BCrypt.Net.BCrypt.HashPassword(userCreateDto.Password);
            var userCreate = _mapper.Map<User>(userCreateDto);

            await _userRepository.AddAsync(userCreate, cancellationToken);

            var user = await _userRepository.GetByIdAsync(userCreate.UserId, cancellationToken);
            var userDto = _mapper.Map<UserDto>(user);

            return new SuccessResponse<UserDto>(userDto);
        }
    }
}
