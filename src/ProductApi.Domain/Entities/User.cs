using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace ProductApi.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public int UserRole { get; set; }
        public bool IsLogin { get; set; }
        public int FalsePwdCount { get; set; }
        public bool IsRevoke { get; set; }
        public bool IsActive { get; set; }
        //public UserRole UserRoleNavigation { get; set; }
        //public ICollection<UserActivity>? UserActivities { get; set; }
    }
}
