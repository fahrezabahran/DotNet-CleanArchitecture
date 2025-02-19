using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Application.DTOs
{
    public class ProductDto
    {
        public required string Name { get; set; }
        public required decimal Price { get; set; }
    }
}
