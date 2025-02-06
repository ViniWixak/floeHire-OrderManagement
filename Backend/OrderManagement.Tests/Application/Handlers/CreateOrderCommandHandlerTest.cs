using Moq;
using OrderManagement.Application.Commands;
using OrderManagement.Application.Handlers.Orders;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Enums;
using OrderManagement.Domain.Interfaces;

namespace OrderManagement.Tests.Application.Handlers
{
    public class CreateOrderCommandHandlerTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<IOrderSyncService> _orderSyncServiceMock;
        private readonly CreateOrderCommandHandler _handler;

        public CreateOrderCommandHandlerTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _orderSyncServiceMock = new Mock<IOrderSyncService>(); 
            _handler = new CreateOrderCommandHandler(_orderRepositoryMock.Object, _orderSyncServiceMock.Object); 
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldCreateOrder()
        {
            // Arrange
            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                OrderDate = DateTime.UtcNow,
                TotalAmount = 100.0m,
                Status = OrderStatus.Pending,
                OrderItems = new List<OrderItem>
                {
                    new OrderItem { ProductId = Guid.NewGuid(), Quantity = 1, UnitPrice = 50.0m },
                    new OrderItem { ProductId = Guid.NewGuid(), Quantity = 1, UnitPrice = 50.0m }
                }
            };

            var command = new CreateOrderCommand(order);

            _orderRepositoryMock.Setup(repo => repo.AddOrderAsync(It.IsAny<Order>())).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(order.CustomerId, result.CustomerId);
            Assert.Equal(order.TotalAmount, result.TotalAmount);
            Assert.Equal(order.Status, result.Status);
            Assert.Equal(order.OrderItems.Count, result.OrderItems.Count);

            _orderRepositoryMock.Verify(repo => repo.AddOrderAsync(It.Is<Order>(o => o.CustomerId == order.CustomerId && o.TotalAmount == order.TotalAmount)), Times.Once);
        }
    }
}
