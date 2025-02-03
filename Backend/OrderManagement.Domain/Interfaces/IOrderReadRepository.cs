using OrderManagement.Domain.Entities;
using OrderManagement.Domain.ReadModel;

namespace OrderManagement.Domain.Interfaces
{
    public interface IOrderReadRepository
    {
        public Task<OrderMongoModel?> GetOrderByIdAsync(Guid orderId);
        public Task<IEnumerable<OrderMongoModel>> GetAllOrdersAsync();
    }
}
