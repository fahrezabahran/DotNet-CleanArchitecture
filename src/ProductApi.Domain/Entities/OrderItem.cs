using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Domain.Entities
{
    public class OrderItem
    {
        public int Id { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }

        public decimal TotalPrice => Quantity * Price;

        public OrderItem(string productName, int quantity, decimal price)
        {
            ProductName = productName;
            Quantity = quantity;
            Price = price;
        }
    }
}
