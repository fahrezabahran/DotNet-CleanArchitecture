using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Application.Dtos
{
    public class UserUpdateDto
    {
        public string? Password { get; set; }
        public string? UserRole { get; set; }
        public bool IsLogin { get; set; }
        public int FalsePwdCount { get; set; }
        public int IsRevoke { get; set; }
        public int IsActive { get; set; }
    }
}
