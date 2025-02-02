using OrderManagement.Domain.Entities;

namespace OrderManagement.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(Guid orderId);

        Task<Order> GetOrderByIdAsync(Guid orderId);
        Task<Order> GetOrderDatailsByIdAsync(Guid orderId);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(Guid customerId);
    }

}
