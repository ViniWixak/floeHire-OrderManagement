﻿using OrderManagement.Domain.Enums;

namespace OrderManagement.Domain.Entities
{
    internal class Order
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderItem> Items { get; set; } = new();
    }
}
