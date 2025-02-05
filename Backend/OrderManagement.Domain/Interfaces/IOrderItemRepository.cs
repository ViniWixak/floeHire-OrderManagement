using OrderManagement.Domain.Entities;

namespace OrderManagement.Domain.Interfaces
{
    public interface IOrderItemRepository
    {
        Task<IEnumerable<OrderItem>> GetOrderItemsAsync(Guid id);
        Task AddOrderItemAsync(OrderItem order);
        Task DeleteOrderItemAsync(Guid id);
    }
}
