using MongoDB.Driver;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces.Mongo;
using OrderManagement.Domain.Models.MongoModel;

namespace OrderManagement.Infrastructure.Repositories
{
    public class OrderWriteRepository : IOrderWriteRepository
    {
        private readonly IMongoCollection<OrderMongoModel> _orderCollection;

        public OrderWriteRepository(IMongoDatabase database)
        {
            _orderCollection = database.GetCollection<OrderMongoModel>("Orders");
        }

        public async Task InsertOrderAsync(OrderMongoModel order)
        {
            await _orderCollection.InsertOneAsync(order);
        }

        public async Task UpdateOrderAsync(OrderMongoModel order)
        {
            var filter = Builders<OrderMongoModel>.Filter.Eq(o => o.OrderId, order.OrderId);
            await _orderCollection.ReplaceOneAsync(filter, order);
        }

        public async Task DeleteOrderAsync(Guid orderId)
        {
            var filter = Builders<OrderMongoModel>.Filter.Eq(o => o.OrderId, orderId);
            await _orderCollection.DeleteOneAsync(filter);
        }

        public async Task SeedDataAsync()
        {
            var orders = new List<OrderMongoModel>
            {
                new OrderMongoModel
                {
                    OrderId = Guid.NewGuid(),
                    OrderDate = DateTime.Now,
                    TotalAmount = 24.00m,
                    Status = "Pending",
                    OrderItems = new List<OrderItemMongoModel>
                    {
                        new OrderItemMongoModel { ProductName = "Calabresa", UnitPrice = 24.00m, Quantity = 1 }
                    }
                },
                new OrderMongoModel
                {
                    OrderId = Guid.NewGuid(),
                    OrderDate = DateTime.Now,
                    TotalAmount = 26.00m,
                    Status = "Completed",
                    OrderItems = new List<OrderItemMongoModel>
                    {
                        new OrderItemMongoModel { ProductName = "Marguerita", UnitPrice = 26.00m, Quantity = 1 }
                    }
                },
                new OrderMongoModel
                {
                    OrderId = Guid.NewGuid(),
                    OrderDate = DateTime.Now,
                    TotalAmount = 40.00m,
                    Status = "Cancelled",
                    OrderItems = new List<OrderItemMongoModel>
                    {
                        new OrderItemMongoModel { ProductName = "Quatro queijos", UnitPrice = 40.00m, Quantity = 1 }
                    }
                }
            };
            await _orderCollection.InsertManyAsync(orders);
            
        }

    }
}

