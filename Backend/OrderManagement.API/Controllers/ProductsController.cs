using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.Commands;
using OrderManagement.Application.Queries;
using OrderManagement.Domain.Entities;

namespace OrderManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("products")]
        public async Task<ActionResult<IEnumerable<string>>> GetProductsAsync()
        {
            var products = await _mediator.Send(new GetAllProductsQuery());
            if(products == null || !products.Any())
            {
                return NotFound();
            }
            return Ok(products);
        }

        [HttpPost]
        [Route("products")]
        public async Task<ActionResult<string>> CreateProductAsync([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Produto não pode ser null.");
            }

            if (string.IsNullOrEmpty(product.Name))
            {
                return BadRequest("Produto não pode ser vazio.");
            }

            if (product.Price <= 0)
            {
                return BadRequest("O preço do produto precisa ser maior que zero");
            }

            product.Id = Guid.NewGuid();

            var result = await _mediator.Send(new CreateProductCommand(product));

            if (result == null)
            {
                return NotFound();
            }

            return CreatedAtAction(nameof(GetProductByIdAsync), new { id = result.Id }, result);
        }

        [HttpPut]
        [Route("products/{id}")]
        public async Task<ActionResult<string>> UpdateProductAsync(Guid id, [FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Produto não pode ser null.");
            }

            if (string.IsNullOrEmpty(product.Name))
            {
                return BadRequest("Produto não pode ser vazio.");
            }

            if (product.Price <= 0)
            {
                return BadRequest("O preço do produto precisa ser maior que zero");
            }

            var existingProduct = await _mediator.Send(new GetProductByIdQuery(id));
            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;

            var result = await _mediator.Send(new UpdateProductByIdCommand(existingProduct));

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("products/{id}")]
        public async Task<ActionResult<Product>> GetProductByIdAsync(Guid id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(id));
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpDelete]
        [Route("products/{id}")]
        public async Task<ActionResult> DeleteProductByIdAsync(Guid id)
        {
            var existingProduct = await _mediator.Send(new GetProductByIdQuery(id));
            if (existingProduct == null)
            {
                return NotFound();
            }

            var result = await _mediator.Send(new DeleteProductByIdCommand(id));

            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }


    }
}
