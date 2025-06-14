﻿using Light_Shop.API.Data;
using Light_Shop.API.DTOs.Requests;
using Light_Shop.API.DTOs.Response;
using Light_Shop.API.Models;
using Light_Shop.API.Services.Interfaces;
using Light_Shop.API.Utility;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Light_Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = $"{StaticData.SuperAdmin}, {StaticData.Admin}, {StaticData.Company}")]

    public class ProductsController : ControllerBase
    {
        IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet("")]
        [AllowAnonymous]
        public IActionResult GetAllProducts([FromQuery] string? query, [FromQuery] int page, [FromQuery] int limit = 10)
        {
            var products = productService.GetAll(query, page, limit);
            return Ok(products.Adapt<IEnumerable<ProductResponse>>());
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
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
        public IActionResult UpdateProduct([FromRoute] int id, [FromForm] ProductUpdateRequest productUpdateRequest)
        {
            var productInDb = productService.Edit(id, productUpdateRequest);
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
