using OrderManagement.Domain.Interfaces;
using OrderManagement.Domain.Interfaces.Mongo;
using OrderManagement.Domain.Models.MongoModel;

namespace OrderManagement.Application.Services
{
    public class OrderSyncService : IOrderSyncService
    {
        private readonly IOrderWriteRepository _orderWriteRepository;
        private readonly IOrderReadRepository _orderReadRepository;

        public OrderSyncService(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository)
        {
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
        }

        public async Task SyncOrderToMongo(OrderMongoModel order)
        {
            var existingOrder = await _orderReadRepository.GetOrderByIdAsync(order.OrderId);

            if (existingOrder == null)
            {
                await _orderWriteRepository.InsertOrderAsync(order);
            }
            else
            {
                await _orderWriteRepository.UpdateOrderAsync(order);
            }
        }

        public async Task DeleteOrderFromMongo(Guid OrderId)
        {
            var existingOrder = await _orderReadRepository.GetOrderByIdAsync(OrderId);

            if (existingOrder == null)
            {
                return;
            }
            else
            {
                await _orderWriteRepository.DeleteOrderAsync(OrderId);
            }
        }

        public async Task UpdateOrderFromMongo(OrderMongoModel order)
        {
            var existingOrder = await _orderReadRepository.GetOrderByIdAsync(order.OrderId);
            if (existingOrder == null)
            {
                return;
            }
            else
            {
                await _orderWriteRepository.UpdateOrderAsync(order);
            }
        }
    }
}