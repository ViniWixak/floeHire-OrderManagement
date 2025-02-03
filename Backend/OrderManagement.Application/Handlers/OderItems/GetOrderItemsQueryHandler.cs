using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OrderManagement.Application.Queries;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;

namespace OrderManagement.Application.Handlers.OderItems
{
    public class GetOrderItemsQueryHandler : IRequestHandler<GetOrderItemsQuery, IEnumerable<OrderItem>>
    {
        private readonly IOrderItemRepository _orderItemRepository;

        public GetOrderItemsQueryHandler(IOrderItemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }

        public async Task<IEnumerable<OrderItem>> Handle(GetOrderItemsQuery request, CancellationToken cancellationToken)
        {
            var orderItems = await _orderItemRepository.GetOrderItemsAsync(request.OrderId);
            return orderItems;
        }
    }
}
