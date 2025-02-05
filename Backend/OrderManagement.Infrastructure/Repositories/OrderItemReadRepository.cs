using MongoDB.Driver;
using OrderManagement.Domain.Interfaces.Mongo;
using OrderManagement.Domain.Models.MongoModel;

namespace OrderManagement.Infrastructure.Repositories
{
    public class OrderItemReadRepository : IOrderItemReadRepository
    {
        private readonly IMongoCollection<OrderItemMongoModel> _orderItemsCollection;

        public OrderItemReadRepository(IMongoDatabase database)
        {
            _orderItemsCollection = database.GetCollection<OrderItemMongoModel>("OrderItems");
        }

        public async Task<IEnumerable<OrderItemMongoModel>> GetOrderItemsByOrderIdAsync(Guid orderId)
        {
            return await _orderItemsCollection.Find(item => item.OrderId == orderId).ToListAsync();
        }

        public async Task<OrderItemMongoModel?> GetOrderItemByIdAsync(string id)
        {
            return await _orderItemsCollection.Find(item => item.Id == id).FirstOrDefaultAsync();
        }
    }
}
