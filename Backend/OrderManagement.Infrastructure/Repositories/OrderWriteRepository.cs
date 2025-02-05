using MongoDB.Driver;
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
    }
}

