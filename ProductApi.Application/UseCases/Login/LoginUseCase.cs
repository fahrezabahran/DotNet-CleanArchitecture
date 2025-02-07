using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductApi.Application.DTOs;
using ProductApi.Domain.Entities;
using ProductApi.Domain.Interfaces;

namespace ProductApi.Application.UseCases.Login
{
    public class LoginUseCase(IGenericRepository<User> userRepository)
    {
        private readonly IGenericRepository<User> _userRepository = userRepository;

        public async Task Execute(UserDto userDto)
        {
            var user = await _userRepository.FindAsync(u => u.UserName.Contains(userDto.UserName));

            if (user == null)
            {
                return;
            }


        }
    }
}
