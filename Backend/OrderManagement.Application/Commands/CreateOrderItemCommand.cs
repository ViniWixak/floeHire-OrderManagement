using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Commands
{
    public class CreateOrderItemCommand : IRequest<OrderItem>
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public CreateOrderItemCommand(Guid orderId, Guid productId, int quantity)
        {
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
