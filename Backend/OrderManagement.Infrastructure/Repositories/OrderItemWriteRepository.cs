using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using OrderManagement.Domain.Interfaces.Mongo;
using OrderManagement.Domain.Models.MongoModel;

namespace OrderManagement.Infrastructure.Repositories
{
    public class OrderItemWriteRepository : IOrderItemWriteRepository
    {
        private readonly IMongoCollection<OrderItemMongoModel> _orderItemsCollection;

        public OrderItemWriteRepository(IMongoDatabase database)
        {
            _orderItemsCollection = database.GetCollection<OrderItemMongoModel>("OrderItems");
        }

        public async Task AddOrderItemAsync(OrderItemMongoModel orderItem)
        {
            await _orderItemsCollection.InsertOneAsync(orderItem);
        }

        public async Task UpdateOrderItemAsync(OrderItemMongoModel orderItem)
        {
            var filter = Builders<OrderItemMongoModel>.Filter.Eq(i => i.Id, orderItem.Id);
            await _orderItemsCollection.ReplaceOneAsync(filter, orderItem);
        }

        public async Task DeleteOrderItemAsync(string id)
        {
            var filter = Builders<OrderItemMongoModel>.Filter.Eq(i => i.Id, id);
            await _orderItemsCollection.DeleteOneAsync(filter);
        }
    }
}
