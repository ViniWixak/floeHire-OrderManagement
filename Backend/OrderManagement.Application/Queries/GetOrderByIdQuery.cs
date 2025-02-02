using MediatR;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Queries
{
    public class GetOrderByIdQuery : IRequest<Order>
    {
        public Guid Id { get; set; }

        public GetOrderByIdQuery(Guid id)
        {
            Id = id;
        }
    }

}
