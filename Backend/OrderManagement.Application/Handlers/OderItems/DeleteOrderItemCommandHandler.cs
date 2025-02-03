using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OrderManagement.Application.Commands;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;

namespace OrderManagement.Application.Handlers.OderItems
{
    public class DeleteOrderItemCommandHandler : IRequestHandler<DeleteOrderItemCommand, OrderItem>
    {
        private readonly IOrderItemRepository _orderItemRepository;

        public DeleteOrderItemCommandHandler(IOrderItemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }

        public async Task<OrderItem> Handle(DeleteOrderItemCommand request, CancellationToken cancellationToken)
        {
            var orderItems = await _orderItemRepository.GetOrderItemsAsync(request.OrderId);
            var orderItem = orderItems.FirstOrDefault(item => item.Id == request.OrderItemId);

            if (orderItem == null)
            {
                throw new Exception("Order item not found");
            }

            await _orderItemRepository.DeleteOrderItemAsync(orderItem.Id);

            return orderItem;
        }
    }
}
