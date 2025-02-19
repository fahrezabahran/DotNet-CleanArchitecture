using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Domain.Entities
{
    public class UserRole
    {
        public int RoleId { get; set; }
        public required string RoleName { get; set; }
        public bool IsActive { get; set; }
        //public ICollection<User> Users { get; set; }
    }
}