using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Application.Dtos
{
    public class TimesheetDto
    {
        public int UserId { get; set; }
        public DateOnly CheckInDate { get; set; }
        public TimeSpan CheckInTime { get; set; }
        public TimeSpan CheckOutTime { get; set; }
        public string? JobDetails { get; set; }
        public string? Remarks { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
