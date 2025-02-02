using Microsoft.AspNetCore.Mvc;

namespace OrderManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        [Route("products")]
        public ActionResult<IEnumerable<string>> GetProducts()
        {
            var products = new List<string> { "Product1", "Product2", "Product3" };
            return Ok(products);
        }
    }
}
