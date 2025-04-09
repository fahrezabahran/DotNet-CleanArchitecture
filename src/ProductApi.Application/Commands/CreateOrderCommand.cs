using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Application.Commands
{
    public class CreateOrderCommand
    {
        public string CustomerName { get; set; }
        public List<CreateOrderItemDto> Items { get; set; }
    }
}
