using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Commands
{
    public class DeleteOrderItemCommand : IRequest<OrderItem>
    {
        public Guid OrderId { get; set; }
        public Guid OrderItemId { get; set; }

        public DeleteOrderItemCommand(Guid orderId, Guid orderItemId)
        {
            OrderId = orderId;
            OrderItemId = orderItemId;
        }
    }
}
