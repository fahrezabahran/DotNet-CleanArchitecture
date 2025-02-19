using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProductApi.Application.DTOs
{
    public class UserCreateDto
    {
        [JsonPropertyName("username")]
        public required string UserName { get; set; }

        [JsonPropertyName("password")]
        public required string Password { get; set; }
    }
}
