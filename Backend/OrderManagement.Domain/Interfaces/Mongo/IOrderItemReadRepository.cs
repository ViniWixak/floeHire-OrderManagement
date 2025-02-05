using OrderManagement.Domain.Models.MongoModel;

namespace OrderManagement.Domain.Interfaces.Mongo
{
    public interface IOrderItemReadRepository
    {
        Task<IEnumerable<OrderItemMongoModel>> GetOrderItemsByOrderIdAsync(Guid orderId);
        Task<OrderItemMongoModel?> GetOrderItemByIdAsync(string id);
    }
}
