using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Domain.Entities
{
    public class UserActivity
    {
        public int ActivityId { get; set; }
        public int UserId { get; set; }
        public bool Login { get; set; }
        public bool Logout { get; set; }
        public bool ChangePassword { get; set; }
        public DateTime ActivityDate { get; set; }
    }
}
