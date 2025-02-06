using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OrderManagement.Domain.Interfaces.Mongo;
using OrderManagement.Domain.Models.MongoModel;
using OrderManagement.Infrastructure.Configuration;

namespace OrderManagement.Infrastructure.Repositories
{
    public class OrderReadRepository : IOrderReadRepository
    {
        private readonly IMongoCollection<OrderMongoModel> _ordersCollection;

        public OrderReadRepository(IMongoClient mongoClient, string databaseName)
        {
            var database = mongoClient.GetDatabase(databaseName);
            _ordersCollection = database.GetCollection<OrderMongoModel>("Orders");
        }

        public async Task<IEnumerable<OrderMongoModel>> GetAllOrdersAsync()
        {
            var orders = await _ordersCollection.FindAsync(order => true);
            return orders.ToList();
        }

        public async Task<OrderMongoModel?> GetOrderByIdAsync(Guid orderId)
        {
            var order = await _ordersCollection.FindAsync(order => order.OrderId == orderId);
            return order.FirstOrDefault();
        }
    }
}
