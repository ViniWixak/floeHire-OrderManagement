using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.Queries;
using OrderManagement.Domain.Models.MongoModel;

namespace OrderManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderReadController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderReadController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Obtém todos os pedidos (Consulta no MongoDB)
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderMongoModel>>> GetAllOrders()
        {
            var orders = await _mediator.Send(new GetAllOrdersFromMongoQuery());

            if (orders == null || !orders.Any())
            {
                return NotFound();
            }

            return Ok(orders);
        }
    }
}
