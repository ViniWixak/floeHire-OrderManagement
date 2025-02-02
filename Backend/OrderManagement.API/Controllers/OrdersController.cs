using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.Queries;
using OrderManagement.Domain.Entities;
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
    }
}
