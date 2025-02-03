using MediatR;
using OrderManagement.Application.Commands;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Domain.ReadModel;

namespace OrderManagement.Application.Handlers.Orders
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Order>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderSyncService _orderSyncService;

        public CreateOrderCommandHandler(
            IOrderRepository orderRepository, IOrderSyncService orderSyncService)
        {
            _orderRepository = orderRepository;
            _orderSyncService = orderSyncService;
        }

        public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = request.CustomerId,
                OrderDate = request.OrderDate,
                TotalAmount = request.TotalAmount,
                OrderItems = request.Items,
                Status = request.Status
            };

            await _orderRepository.AddOrderAsync(order);

            // Instanciar OrderMongoModel
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

            // Sincronizar MongoDB
            await _orderSyncService.SyncOrderToMongo(orderMongoModel);

            return order;
        }
    }
}
