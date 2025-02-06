using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using MongoDB.Driver;
using OrderManagement.Infrastructure.Repositories;
using OrderManagement.Domain.Models;
using Xunit;
using OrderManagement.Domain.Models.MongoModel;

namespace OrderManagement.Tests.Infraestructure.Repository
{
    public class OrderReadRepositoryTests
    {
        private readonly Mock<IMongoCollection<OrderMongoModel>> _mockCollection;
        private readonly Mock<IMongoDatabase> _mockDatabase;
        private readonly Mock<IMongoClient> _mockClient;
        private readonly OrderReadRepository _orderReadRepository;

        public OrderReadRepositoryTests()
        {
            _mockCollection = new Mock<IMongoCollection<OrderMongoModel>>();
            _mockDatabase = new Mock<IMongoDatabase>();
            _mockClient = new Mock<IMongoClient>();

            _mockDatabase
                .Setup(db => db.GetCollection<OrderMongoModel>("Orders", null))
                .Returns(_mockCollection.Object);

            _mockClient
                .Setup(client => client.GetDatabase("OrderDb", null))
                .Returns(_mockDatabase.Object);

            _orderReadRepository = new OrderReadRepository(_mockClient.Object, "OrderDb");
        }

        [Fact]
        public async Task GetOrderByIdAsync_ShouldReturnOrder_WhenOrderExists()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var order = new OrderMongoModel { OrderId = orderId };

            var mockCursor = new Mock<IAsyncCursor<OrderMongoModel>>();
            mockCursor.SetupSequence(c => c.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);
            mockCursor.SetupGet(c => c.Current).Returns(new List<OrderMongoModel> { order });

            _mockCollection
                .Setup(c => c.FindAsync(
                    It.IsAny<FilterDefinition<OrderMongoModel>>(),
                    It.IsAny<FindOptions<OrderMongoModel, OrderMongoModel>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockCursor.Object);

            // Act
            var result = await _orderReadRepository.GetOrderByIdAsync(orderId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(orderId, result.OrderId);
        }

        [Fact]
        public async Task GetAllOrdersAsync_ShouldReturnOrders_WhenOrdersExist()
        {
            // Arrange
            var orders = new List<OrderMongoModel>
        {
            new OrderMongoModel { OrderId = Guid.NewGuid() },
            new OrderMongoModel { OrderId = Guid.NewGuid() }
        };

            var mockCursor = new Mock<IAsyncCursor<OrderMongoModel>>();
            mockCursor.SetupSequence(c => c.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);
            mockCursor.SetupGet(c => c.Current).Returns(orders);

            _mockCollection
                .Setup(c => c.FindAsync(
                    It.IsAny<FilterDefinition<OrderMongoModel>>(),
                    It.IsAny<FindOptions<OrderMongoModel, OrderMongoModel>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockCursor.Object);

            // Act
            var result = await _orderReadRepository.GetAllOrdersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(orders.Count, result.Count());
        }
    }
}
