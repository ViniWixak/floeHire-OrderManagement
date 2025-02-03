using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.Commands;
using OrderManagement.Application.Handlers.OderItems;
using OrderManagement.Application.Queries;

namespace OrderManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("orders/{id}/items")]
        public async Task<IActionResult> GetOrderItems(Guid id)
        {
            var result = await _mediator.Send(new GetOrderItemsQuery(id));
            return Ok(result);
        }

        [HttpPost("orders/{id}/items")]
        public async Task<IActionResult> AddOrderItem(Guid id, [FromBody] CreateOrderItemCommand command)
        {
            var order = await _mediator.Send(new GetOrderByIdQuery(id));

            if (order == null)
            {
                return NotFound("Pedido não encontrado.");
            }

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetOrderItems), new { id = order.Id }, result);
        }

        [HttpDelete("orders/{id}/items/{itemId}")]
        public async Task<IActionResult> DeleteOrderItem(Guid orderId, Guid itemId)
        {
            var order = await _mediator.Send(new GetOrderByIdQuery(orderId));

            if (order == null)
            {
                return NotFound("Pedido não encontrado.");
            }

            var result = await _mediator.Send(new DeleteOrderItemCommand(orderId, itemId));

            if (result == null)
            {
                return NotFound("Item do pedido não encontrado.");
            }

            return NoContent();
        }


        
    }
}
