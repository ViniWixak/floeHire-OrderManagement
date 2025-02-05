using OrderManagement.Domain.Models.MongoModel;

namespace OrderManagement.Domain.Interfaces.Mongo
{
    public interface IOrderWriteRepository
    {
        Task InsertOrderAsync(OrderMongoModel order);
        Task UpdateOrderAsync(OrderMongoModel order);
        Task DeleteOrderAsync(Guid orderId);
    }

}
