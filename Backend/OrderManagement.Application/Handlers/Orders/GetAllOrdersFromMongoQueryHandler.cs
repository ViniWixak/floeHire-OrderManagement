using MediatR;
using OrderManagement.Application.Queries;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Domain.Interfaces.Mongo;
using OrderManagement.Domain.Models.MongoModel;

namespace OrderManagement.Application.Handlers.Orders
{
    public class GetAllOrdersFromMongoQueryHandler : IRequestHandler<GetAllOrdersFromMongoQuery, IEnumerable<OrderMongoModel>>
    {
        private readonly IOrderReadRepository _orderReadRepository;

        public GetAllOrdersFromMongoQueryHandler(IOrderReadRepository orderReadRepository)
        {
            _orderReadRepository = orderReadRepository; 
        }

        public async Task<IEnumerable<OrderMongoModel>> Handle(GetAllOrdersFromMongoQuery request, CancellationToken cancellationToken)
        {
            return await _orderReadRepository.GetAllOrdersAsync();
            
        }
    }
}
