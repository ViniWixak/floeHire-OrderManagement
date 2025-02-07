using MediatR;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Enums;

namespace OrderManagement.Application.Commands
{
    public class UpdateOrderByIdCommand : IRequest<Order>
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        public UpdateOrderByIdCommand(Order order)
        {
            Id = order.Id;
            CustomerId = order.CustomerId;           
            OrderItems = order.OrderItems;
            TotalAmount = order.TotalAmount;
            OrderDate = order.OrderDate;
            Status = order.Status;
        }
    }
}
