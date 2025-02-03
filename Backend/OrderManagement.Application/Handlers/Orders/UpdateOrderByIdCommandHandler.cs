using MediatR;
using OrderManagement.Application.Commands;
using OrderManagement.Application.Queries;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Domain.ReadModel;

namespace OrderManagement.Application.Handlers.Orders
{
    public class UpdateOrderByIdCommandHandler : IRequestHandler<UpdateOrderByIdCommand, Order>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderSyncService _orderSyncService;

        public UpdateOrderByIdCommandHandler(IOrderRepository orderRepository, IOrderSyncService orderSyncService)
        {
            _orderRepository = orderRepository;
            _orderSyncService = orderSyncService;
        }

        public async Task<Order> Handle(UpdateOrderByIdCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderByIdAsync(request.Id);
            if (order == null)
            {
                return null;
            }

            order.CustomerId = request.CustomerId;
            order.OrderDate = request.OrderDate;
            order.TotalAmount = request.TotalAmount;
            order.OrderItems = request.OrderItems;
            order.Status = request.Status;

            await _orderRepository.UpdateOrderAsync(order);

            var orderMongoModel = new OrderMongoModel
            {
                OrderId = order.Id,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                OrderItems = order.OrderItems.Select(item => new OrderItemReadModel
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    TotalPrice = item.TotalPrice
                }).ToList(),
                Status = order.Status.ToString()
            };

            await _orderSyncService.UpdateOrderFromMongo(orderMongoModel);

            return order;
        }
    }
}
