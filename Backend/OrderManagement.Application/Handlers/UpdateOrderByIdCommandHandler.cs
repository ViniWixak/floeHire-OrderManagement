using MediatR;
using OrderManagement.Application.Commands;
using OrderManagement.Application.Queries;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;

namespace OrderManagement.Application.Handlers
{
    public class UpdateOrderByIdCommandHandler : IRequestHandler<UpdateOrderByIdCommand, Order>
    {
        private readonly IOrderRepository _orderRepository;

        public UpdateOrderByIdCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> Handle(UpdateOrderByIdCommand request, CancellationToken cancellationToken)
        {
            var originalOrder = await _orderRepository.GetOrderByIdAsync(request.Id);
            if (originalOrder == null)
            {
                return null;
            }

            var updatedOrder = new Order
            {
                Id = originalOrder.Id,
                CustomerId = request.CustomerId,
                OrderDate = request.OrderDate,
                TotalAmount = request.TotalAmount,
                OrderItems = request.OrderItems,
                Status = request.Status
            };

            await _orderRepository.UpdateOrderAsync(updatedOrder);
            return updatedOrder;
        }
    }
}
