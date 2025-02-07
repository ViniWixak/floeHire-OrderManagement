using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.Commands;
using OrderManagement.Application.Handlers;
using OrderManagement.Application.Queries;
using OrderManagement.Domain.DTOs;
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
        public async Task<IActionResult> AddOrder([FromBody] OrderDto orderDto)
        {
            if (orderDto == null || orderDto.Products == null || !orderDto.Products.Any())
            {
                return BadRequest("Os dados do pedido são inválidos.");
            }

            var orderId = Guid.NewGuid();

            var order = new Order
            {
                Id = orderId,
                CustomerId = Guid.Parse("BEB70355-350D-4C03-B3DD-608C2D7C6ECB"),
                TotalAmount = orderDto.TotalAmount,
                OrderItems = orderDto.Products.Select(p => new OrderItem
                {
                    Id = Guid.NewGuid(),
                    OrderId = orderId,
                    ProductName = p.Name,
                    ProductId = p.ProductId,
                    Quantity = p.Quantity,
                    TotalPrice = p.TotalPrice
                }).ToList(),
                OrderDate = DateTime.Now,
                Status = OrderStatus.Pending
            };

            var result = await _mediator.Send(new CreateOrderCommand(order));

            if (result == null)
            {
                return BadRequest("Não foi possível criar o pedido.");
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
        public async Task<IActionResult> UpdateOrderStatus(Guid id, [FromBody] UpdateOrderStatusRequestDto request)
        {
            var order = await _mediator.Send(new GetOrderByIdQuery(id));

            if (order == null)
            {
                return NotFound();
            }

            order.Status = (OrderStatus)request.Status;
            await _mediator.Send(new UpdateOrderByIdCommand(order));

            return NoContent(); 
        }

    }
}
