using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Application.Models
{
    public class ApiResponse<T>
    {
        public required bool Success { get; set; }

        public required string Message { get; set; }

        public T? Data { get; set; }
    }
}
