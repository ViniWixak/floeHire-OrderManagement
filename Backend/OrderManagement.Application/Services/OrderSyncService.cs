using MongoDB.Driver;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Domain.Interfaces.Mongo;
using OrderManagement.Domain.Models.MongoModel;

namespace OrderManagement.Application.Services
{
    public class OrderSyncService : IOrderSyncService
    {
        private readonly IOrderWriteRepository _orderWriteRepository;
        private readonly IOrderReadRepository _orderReadRepository;
        private readonly IMongoCollection<OrderMongoModel> _mongoCollection;

        public OrderSyncService(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository, IMongoCollection<OrderMongoModel> mongoCollection)
        {
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
            _mongoCollection = mongoCollection;
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
                //await _orderWriteRepository.UpdateOrderAsync(order);


                // Filtrando pelo OrderId em vez de Id (que é o campo imutável)
                var filter = Builders<OrderMongoModel>.Filter.Eq(o => o.OrderId, order.OrderId);

                var update = Builders<OrderMongoModel>.Update
                    .Set(o => o.OrderDate, order.OrderDate)
                    .Set(o => o.TotalAmount, order.TotalAmount)
                    .Set(o => o.OrderItems, existingOrder.OrderItems)
                    .Set(o => o.Status, order.Status);

                // Aplicar a atualização no MongoDB sem tocar no campo _id
                await _mongoCollection.UpdateOneAsync(filter, update);
            }
        }

    }
}