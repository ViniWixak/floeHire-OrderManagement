using MediatR;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Application.Queries
{
    public class GetAllOrdersQuery : IRequest<List<Order>>
    {
    }

}
