using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Queries
{
    public class GetOrderItemsQuery : IRequest<IEnumerable<OrderItem>>
    {
        public Guid OrderId { get; set; }

        public GetOrderItemsQuery(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
