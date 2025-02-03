using OrderManagement.Domain.Entities;
using OrderManagement.Domain.ReadModel;

namespace OrderManagement.Domain.Interfaces
{
    public interface IOrderSyncService
    {
        Task SyncOrderToMongo(OrderMongoModel order);
        Task DeleteOrderFromMongo(Guid OrderId);
        Task UpdateOrderFromMongo(OrderMongoModel order);
    }
}
