using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OrderManagement.API.Controllers;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Enums;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Domain.Models.MongoModel;
namespace OrderManagement.Tests.Controllers
{
    public class OrderControllerTests
    {
        private readonly Mock<IOrderSyncService> _mockOrderSyncService;
        private readonly Mock<IMediator> _mockMediator; 
        private readonly OrdersController _orderController;

        public OrderControllerTests()
        {
            _mockOrderSyncService = new Mock<IOrderSyncService>();
            _mockMediator = new Mock<IMediator>(); 
            _orderController = new OrdersController(_mockMediator.Object);
        }

        [Fact]
        public async Task GetOrderById_ShouldReturnOrder_WhenOrderExists()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var order = new Order { Id = orderId, CustomerId = Guid.NewGuid(), OrderDate = DateTime.UtcNow, TotalAmount = 100, Status = OrderStatus.Pending, OrderItems = new List<OrderItem>() };
            _mockMediator.Setup(m => m.Send(It.IsAny<IRequest<Order>>(), It.IsAny<CancellationToken>())).ReturnsAsync(order);

            // Act
            var result = await _orderController.GetOrderById(orderId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Order>(okResult.Value);
            Assert.Equal(orderId, returnValue.Id);
        }

        [Fact]
        public async Task GetOrderById_ShouldReturnNotFound_WhenOrderDoesNotExist()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            _mockMediator.Setup(m => m.Send(It.IsAny<IRequest<Order>>(), It.IsAny<CancellationToken>())).ReturnsAsync((Order)null);

            // Act
            var result = await _orderController.GetOrderById(orderId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetAllOrders_ShouldReturnOrders_WhenOrdersExist()
        {
            // Arrange
            var orders = new List<Order>
            {
                new Order { Id = Guid.NewGuid(), CustomerId = Guid.NewGuid(), OrderDate = DateTime.UtcNow, TotalAmount = 100, Status = OrderStatus.Pending, OrderItems = new List<OrderItem>() },
                new Order { Id = Guid.NewGuid(), CustomerId = Guid.NewGuid(), OrderDate = DateTime.UtcNow, TotalAmount = 200, Status = OrderStatus.Paid, OrderItems = new List<OrderItem>() }
            };
            _mockMediator.Setup(m => m.Send(It.IsAny<IRequest<List<Order>>>(), It.IsAny<CancellationToken>())).ReturnsAsync(orders);

            // Act
            var result = await _orderController.GetAllOrders();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Order>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetAllOrders_ShouldReturnNotFound_WhenNoOrdersExist()
        {
            // Arrange
            var orders = new List<Order>();
            _mockMediator.Setup(m => m.Send(It.IsAny<IRequest<List<Order>>>(), It.IsAny<CancellationToken>())).ReturnsAsync(orders);

            // Act
            var result = await _orderController.GetAllOrders();

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
