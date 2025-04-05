using Light_Shop.API.Data;
using Light_Shop.API.DTOs.Requests;
using Light_Shop.API.DTOs.Response;
using Light_Shop.API.Models;
using Light_Shop.API.Services.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Light_Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet("")]
        public IActionResult GetAllProducts()
        {
            var products = productService.GetAll();
            return Ok(products.Adapt<IEnumerable<ProductResponse>>());
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById([FromRoute] int id)
        {
            var product = productService.Get(e => e.Id == id);
            if(product == null) return NotFound();
            return Ok(product.Adapt<ProductResponse>());
        }

        [HttpPost("")]
        public IActionResult CreateProduct([FromForm] ProductRequest productRequest)
        {
            var productInDb = productService.Add(productRequest);
            return CreatedAtAction(nameof(GetProductById), new { productInDb.Id }, productInDb.Adapt<ProductResponse>());
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct([FromRoute] int id, [FromForm] ProductRequest productRequest)
        {
            var productInDb = productService.Edit(id, productRequest);
            if (!productInDb) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProductById([FromRoute] int id)
        {
            var deleted = productService.Remove(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
