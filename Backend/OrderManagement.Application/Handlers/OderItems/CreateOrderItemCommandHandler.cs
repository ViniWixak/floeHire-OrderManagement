using MediatR;
using OrderManagement.Application.Commands;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Domain.Interfaces.Mongo;
using OrderManagement.Domain.Models.MongoModel;

namespace OrderManagement.Application.Handlers.OderItems
{
    public class CreateOrderItemCommandHandler : IRequestHandler<CreateOrderItemCommand, OrderItem>
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderItemWriteRepository _orderItemWriteRepository;

        public CreateOrderItemCommandHandler(IOrderItemRepository orderItemRepository, IProductRepository productRepository, IOrderItemWriteRepository orderItemWriteRepository)
        {
            _orderItemRepository = orderItemRepository;
            _productRepository = productRepository;
            _orderItemWriteRepository = orderItemWriteRepository;
        }

        public async Task<OrderItem> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByIdAsync(request.ProductId);

            if (product == null)
            {
                return null;
            }

            var orderItem = new OrderItem
            {
                Id = Guid.NewGuid(),
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                OrderId = request.OrderId,
                UnitPrice = product.Price,
                ProductName = product.Name,
                TotalPrice = product.Price * request.Quantity
            };

            var orderItemReadModel = new OrderItemMongoModel
            {
                Id = orderItem.Id.ToString(),
                OrderId = orderItem.OrderId,
                ProductId = orderItem.ProductId,
                ProductName = orderItem.ProductName,
                Quantity = orderItem.Quantity,
                UnitPrice = orderItem.UnitPrice,
                TotalPrice = orderItem.TotalPrice
            };

            await _orderItemRepository.AddOrderItemAsync(orderItem);
            await _orderItemWriteRepository.AddOrderItemAsync(orderItemReadModel);

            return orderItem;
        }
    }
}
