using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.Commands;
using OrderManagement.Application.Handlers;
using OrderManagement.Application.Queries;
using OrderManagement.Domain.Entities;
using OrderManagement.Domain.Enums;
using System;
using System.Threading.Tasks;

namespace OrderManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _mediator.Send(new GetAllOrdersQuery());

            if (orders == null || !orders.Any())
            {
                return NotFound();
            }

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var order = await _mediator.Send(new GetOrderByIdQuery(id));

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] Order order)
        {
            order.CustomerId = Guid.Parse("BEB70355-350D-4C03-B3DD-608C2D7C6ECB");
            var result = await _mediator.Send(new CreateOrderCommand(order));

            if (result == null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetOrderById), new { id = result.Id }, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderById(Guid id)
        {
            var result = await _mediator.Send(new DeleteOrderByIdCommand(id));

            if (result is null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrder([FromBody] Order order)
        {
            var result = await _mediator.Send(new UpdateOrderByIdCommand(order));

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> CancelOrder(Guid id)
        {
            var order = await _mediator.Send(new GetOrderByIdQuery(id));

            if (order == null)
            {
                return NotFound();
            }

            order.Status = OrderStatus.Canceled;
            var result = await _mediator.Send(new UpdateOrderByIdCommand(order));

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateOrderStatus(Guid id, [FromBody] OrderStatus status)
        {
            var order = await _mediator.Send(new GetOrderByIdQuery(id));

            if (order == null)
            {
                return NotFound();
            }

            order.Status = status;
            var result = await _mediator.Send(new UpdateOrderByIdCommand(order));

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        
    }
}
