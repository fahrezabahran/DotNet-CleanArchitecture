using NUnit.Framework;
using ProductApi.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ProductApi.Tests
{
    [TestFixture]
    public class OrderTests
    {
        private Order _order;

        [SetUp]
        public void Setup()
        {
            var items = new List<OrderItem>
            {
                new OrderItem("Mouse", 1, 100),
                new OrderItem("Keyboard", 2, 150)
            };

            _order = new Order("Alice", items);
        }

        [Test]
        public void OrderItem_TotalPrice_ShouldBeCalculatedCorrectly()
        {
            var item = new OrderItem("Monitor", 2, 500);
            Assert.That(item.TotalPrice, Is.EqualTo(1000));
        }

        [Test]
        public void Order_TotalAmount_ShouldBeSumOfOrderItems()
        {
            Assert.That(_order.TotalAmount, Is.EqualTo(400)); // 100 + (2*150)
            Assert.That(_order.Items.Count, Is.EqualTo(2));
        }

        [Test]
        public void AddItem_ShouldIncreaseTotalAmount()
        {
            var newItem = new OrderItem("Headset", 1, 200);
            _order.AddItem(newItem);

            Assert.That(_order.Items.Count, Is.EqualTo(3));
            Assert.That(_order.TotalAmount, Is.EqualTo(600)); // 400 + 200
        }

        [Test]
        public void RemoveItem_ShouldDecreaseTotalAmount()
        {
            var itemToRemove = _order.Items[0]; // Mouse (100)
            _order.RemoveItem(itemToRemove);

            Assert.That(_order.Items.Count, Is.EqualTo(1));
            Assert.That(_order.TotalAmount, Is.EqualTo(300)); // 400 - 100
        }

        [Test]
        public void OrderItem_ShouldThrowException_WhenInvalidQuantity()
        {
            var ex = Assert.Throws<ArgumentException>(() => new OrderItem("USB", 0, 10));
            Assert.That(ex.Message, Does.Contain("Quantity"));
        }
    }
}
