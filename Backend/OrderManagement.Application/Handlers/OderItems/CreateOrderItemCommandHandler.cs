using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OrderManagement.Application.Commands;
using OrderManagement.Application.Queries;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;

namespace OrderManagement.Application.Handlers.OderItems
{
    public class CreateOrderItemCommandHandler : IRequestHandler<CreateOrderItemCommand, OrderItem>
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IProductRepository _productRepository;

        public CreateOrderItemCommandHandler(IOrderItemRepository orderItemRepository, IProductRepository productRepository)
        {
            _orderItemRepository = orderItemRepository;
            _productRepository = productRepository;
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

            await _orderItemRepository.AddOrderItemAsync(orderItem);

            return orderItem;
        }
    }
}
