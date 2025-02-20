using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Domain.Entities
{
    public class TimeSheet
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateOnly CheckInDate { get; set; }
        public TimeSpan CheckInTime { get; set; }
        public TimeSpan CheckOutTime { get; set; }
        public TimeSpan TotalTime { get; set; }
        public TimeSpan OverTime { get; set; }
        public string? JobDetails { get; set; }
        public string? Remarks { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
    }

}
