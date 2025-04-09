using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProductApi.Application.Dtos
{
    public class UserDto
    {
        public int UserId { get; set; }
        public required string UserName { get; set; }
        public int UserRole { get; set; }
        public bool IsLogin { get; set; }
        public int FalsePwdCount { get; set; }
        public bool IsRevoke { get; set; }
        public bool IsActive { get; set; }
    }
}
