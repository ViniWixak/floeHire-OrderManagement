using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Enums;
using OrderManagement.Infrastructure.Data;
using OrderManagement.Infrastructure.Repositories;

namespace OrderManagement.Tests.Infraestructure.Repository
{
    public class OrderRepositoryTests
    {
        private readonly OrderRepository _orderRepository;
        private readonly DbContextOptions<AppDbContext> _options;
        public OrderRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            var mockAppDbContext = new AppDbContext(_options);

            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(config => config["MongoSettings:ConnectionString"])
                             .Returns("mongodb://localhost:27017");

            // Instancia diretamente em vez de mockar
            var mongoDbContext = new MongoDbContext(mockConfiguration.Object);

            _orderRepository = new OrderRepository(mockAppDbContext, mongoDbContext);
        }

        [Fact]
        public async Task AddOrderAsync_ShouldSaveOrderSuccessfully()
        {
            // Arrange
            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                OrderDate = DateTime.UtcNow,
                TotalAmount = 100.00m,
                Status = OrderStatus.Pending,
                OrderItems = new List<OrderItem>
                            {
                                new OrderItem { ProductId = Guid.NewGuid(), Quantity = 1, UnitPrice = 50.00m },
                                new OrderItem { ProductId = Guid.NewGuid(), Quantity = 1, UnitPrice = 50.00m }
                            }
            };

            // Act
            await _orderRepository.AddOrderAsync(order);
            var savedOrder = await _orderRepository.GetOrderByIdAsync(order.Id);

            // Assert
            Assert.NotNull(savedOrder);
            Assert.Equal(order.Id, savedOrder.Id);
            Assert.Equal(order.CustomerId, savedOrder.CustomerId);
            Assert.Equal(order.OrderDate, savedOrder.OrderDate);
            Assert.Equal(order.TotalAmount, savedOrder.TotalAmount);
            Assert.Equal(order.Status, savedOrder.Status);
            Assert.Equal(order.OrderItems.Count, savedOrder.OrderItems.Count);
        }
    }
}
