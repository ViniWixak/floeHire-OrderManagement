using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OrderManagement.Application.Commands;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Domain.Interfaces.Mongo;

namespace OrderManagement.Application.Handlers.OderItems
{
    public class DeleteOrderItemCommandHandler : IRequestHandler<DeleteOrderItemCommand, OrderItem>
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IOrderItemWriteRepository _orderItemWriteRepository;

        public DeleteOrderItemCommandHandler(IOrderItemRepository orderItemRepository, IOrderItemWriteRepository orderItemWriteRepository)
        {
            _orderItemRepository = orderItemRepository;
            _orderItemWriteRepository = orderItemWriteRepository;
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
            await _orderItemWriteRepository.DeleteOrderItemAsync(orderItem.Id.ToString());

            return orderItem;
        }
    }
}
