using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProductApi.Application.Dtos;
using ProductApi.Application.Interfaces;
using ProductApi.Application.Responses;
using ProductApi.Domain.Entities;

namespace ProductApi.Application.UseCases.UserUseCase
{
    public class GetUsersUseCase(IGenericRepository<User> userRepository, IMapper mapper)
    {
        private readonly IGenericRepository<User> _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse> Execute(CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync(cancellationToken);

            if (!users.Any()) 
            {
                return new SuccessResponse<IEnumerable<UserDto>>([], "No users found, but the operation was successful.");
            }

            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);

            return new SuccessResponse<IEnumerable<UserDto>>(usersDto, "Users retrieved successfully.");
        }

    }
}
