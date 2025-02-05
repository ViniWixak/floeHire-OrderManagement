using OrderManagement.Domain.Models.MongoModel;

namespace OrderManagement.Domain.Interfaces.Mongo
{
    public interface IOrderReadRepository
    {
        public Task<OrderMongoModel?> GetOrderByIdAsync(Guid orderId);
        public Task<IEnumerable<OrderMongoModel>> GetAllOrdersAsync();
    }
}
