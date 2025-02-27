﻿using MediatR;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Enums;

namespace OrderManagement.Application.Commands
{
    public class CreateOrderCommand : IRequest<Order>
    {
        public Guid CustomerId { get; set; }
        public List<OrderItem> Items { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }

        public CreateOrderCommand(Order order)
        {
            CustomerId = order.CustomerId;
            Items = order.OrderItems;
            TotalAmount = order.TotalAmount;
            OrderDate = order.OrderDate;
            Status = order.Status;
        }
    }
}
