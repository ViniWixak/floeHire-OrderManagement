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

        public OrderReadRepository(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _ordersCollection = mongoDatabase.GetCollection<OrderMongoModel>(mongoDbSettings.Value.OrdersCollection);
        }

        public async Task<IEnumerable<OrderMongoModel>> GetAllOrdersAsync()
        {
            try
            {
                return await _ordersCollection.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ocorreu um erro ao buscar todas as ordens no MongoDb.", ex);
            }
        }

        public async Task<OrderMongoModel?> GetOrderByIdAsync(Guid orderId)
        {
            try
            {
                return await _ordersCollection.Find(o => o.OrderId == orderId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Ocorreu um erro ao buscar a ordem com ID {orderId} no MongoDb.", ex);
            }
        }

    }
}
