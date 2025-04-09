using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApi.Domain.Entities
{
    public class Order
    {
        public int Id { get; private set; }
        public string CustomerName { get; private set; }
        public DateTime OrderDate { get; private set; }
        public decimal TotalAmount { get; private set; }
        public List<OrderItem> Items { get; private set; }

        public Order() { }

        public Order(string customerName, List<OrderItem> items)
        {
            CustomerName = customerName;
            Items = items;
            OrderDate = DateTime.UtcNow;
            TotalAmount = items.Sum(item => item.TotalPrice);
        }

        public void AddItem(OrderItem item)
        {
            Items.Add(item);
            TotalAmount += item.TotalPrice;
        }

        public void RemoveItem(OrderItem item)
        {
            Items.Remove(item);
            TotalAmount -= item.TotalPrice;
        }
    }
}
