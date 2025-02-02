using MediatR;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Commands
{
    public class DeleteOrderByIdCommand : IRequest<Order>
    {
        public Guid Id { get; set; }

        public DeleteOrderByIdCommand(Guid id)
        {
            Id = id;
        }
    }
}
