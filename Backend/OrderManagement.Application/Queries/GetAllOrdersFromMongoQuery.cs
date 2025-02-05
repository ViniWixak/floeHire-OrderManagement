using MediatR;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Models.MongoModel;

namespace OrderManagement.Application.Queries
{
    public class GetAllOrdersFromMongoQuery : IRequest<IEnumerable<OrderMongoModel>>
    {
    }

}
