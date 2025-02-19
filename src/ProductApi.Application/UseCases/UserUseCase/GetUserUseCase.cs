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
    public class GetUserUseCase(IGenericRepository<User> userRepository, IMapper mapper)
    {
        private readonly IGenericRepository<User> _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;
        
        public async Task<BaseResponse> Execute(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return new ErrorResponse("User Not Found");

            var userDto = _mapper.Map<UserDto>(user);

            return new SuccessResponse<UserDto>(userDto);
        }
    }
}
