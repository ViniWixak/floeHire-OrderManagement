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
                    OrderId = Guid.Parse("e5475b04-4b6a-4f0f-ab6d-ae3a274ff9fc"),
                    OrderDate = new DateTime(2025, 2, 7, 15, 38, 23, 1, DateTimeKind.Unspecified),
                    TotalAmount = 24.00m,
                    Status = "Pending",
                    OrderItems = new List<OrderItemMongoModel>
                    {
                        new OrderItemMongoModel
                        {
                            ProductId = Guid.Parse("19bb2513-fedf-44b6-c44c-08dd43d9cd34"),
                            ProductName = "Calabresa",
                            UnitPrice = 24.00m,
                            Quantity = 1,
                            TotalPrice = 24.00m
                        }
                    }
                },
                new OrderMongoModel
                {
                    OrderId = Guid.Parse("109909ef-a52c-4aa7-a9f5-9cd0df8f48e7"),
                    OrderDate = new DateTime(2025, 2, 7, 15, 38, 23, 1, DateTimeKind.Unspecified),
                    TotalAmount = 26.00m,
                    Status = "Shipped",
                    OrderItems = new List<OrderItemMongoModel>
                    {
                        new OrderItemMongoModel
                        {
                            ProductId = Guid.Parse("1A44F41F-B7D2-41C9-9497-0BC2C1A2613B"),
                            ProductName = "Marguerita",
                            UnitPrice = 26.00m,
                            Quantity = 1,
                            TotalPrice = 26.00m
                        }
                    }
                },
                new OrderMongoModel
                {
                    OrderId = Guid.Parse("1cb90969-73e3-4d34-94a2-48acc8300062"),
                    OrderDate = new DateTime(2025, 2, 7, 15, 38, 23, 0, DateTimeKind.Unspecified),
                    TotalAmount = 80.00m,
                    Status = "Canceled",
                    OrderItems = new List<OrderItemMongoModel>
                    {
                        new OrderItemMongoModel
                        {
                            ProductId = Guid.Parse("f04acbb2-9c97-4fae-853b-e1c1e52715a2"),
                            ProductName = "Quatro queijos",
                            UnitPrice = 40.00m,
                            Quantity = 2,
                            TotalPrice = 80.00m
                        }
                    }
                }
            };
            await _orderCollection.InsertManyAsync(orders);
        }
    }
}

