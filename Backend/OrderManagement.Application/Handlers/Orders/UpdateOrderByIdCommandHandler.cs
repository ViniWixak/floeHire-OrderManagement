using MediatR;
using OrderManagement.Application.Commands;
using OrderManagement.Application.Queries;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;

namespace OrderManagement.Application.Handlers.Orders
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
            var order = await _orderRepository.GetOrderByIdAsync(request.Id);
            if (order == null)
            {
                return null;
            }

            order.CustomerId = request.CustomerId;
            order.OrderDate = request.OrderDate;
            order.TotalAmount = request.TotalAmount;
            order.OrderItems = request.OrderItems;
            order.Status = request.Status;
            
            await _orderRepository.UpdateOrderAsync(order);
            return order;
        }
    }
}
