using MediatR;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Commands
{
    public class CreateOrderCommand : IRequest<Order>
    {
        public Guid CustomerId { get; set; }
        public List<OrderItem> Items { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
