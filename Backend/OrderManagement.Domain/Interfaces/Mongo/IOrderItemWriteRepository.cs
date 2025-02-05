using OrderManagement.Domain.Models.MongoModel;

namespace OrderManagement.Domain.Interfaces.Mongo
{
    public interface IOrderItemWriteRepository
    {
        Task AddOrderItemAsync(OrderItemMongoModel orderItem);
        Task UpdateOrderItemAsync(OrderItemMongoModel orderItem);
        Task DeleteOrderItemAsync(string id);
    }
}
