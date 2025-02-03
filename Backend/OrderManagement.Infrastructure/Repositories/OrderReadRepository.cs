using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Domain.ReadModel;
using OrderManagement.Infrastructure.Configuration;

namespace OrderManagement.Infrastructure.Repositories
{
    public class OrderReadRepository : IOrderReadRepository
    {
        private readonly IMongoCollection<OrderMongoModel> _ordersCollection;

        public OrderReadRepository(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _ordersCollection = mongoDatabase.GetCollection<OrderMongoModel>(mongoDbSettings.Value.OrdersCollection);
        }

        public async Task<IEnumerable<OrderMongoModel>> GetAllOrdersAsync()
        {
            return await _ordersCollection.Find(_ => true).ToListAsync();
        }

        public async Task<OrderMongoModel?> GetOrderByIdAsync(Guid orderId)
        {
            return await _ordersCollection.Find(o => o.OrderId == orderId).FirstOrDefaultAsync();
        }

    }
}
