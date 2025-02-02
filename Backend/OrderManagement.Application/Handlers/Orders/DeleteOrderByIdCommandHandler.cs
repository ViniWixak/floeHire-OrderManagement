using MediatR;
using OrderManagement.Application.Commands;
using OrderManagement.Application.Queries;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;

namespace OrderManagement.Application.Handlers.Orders
{
    public class DeleteOrderByIdCommandHandler : IRequestHandler<DeleteOrderByIdCommand, Order>
    {
        private readonly IOrderRepository _orderRepository;

        public DeleteOrderByIdCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> Handle(DeleteOrderByIdCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderByIdAsync(request.Id);
            if (order == null)
            {
                return null;
            }

            await _orderRepository.DeleteOrderAsync(request.Id);
            return order;
        }
    }
}
