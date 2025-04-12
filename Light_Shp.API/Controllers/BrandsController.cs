using Light_Shop.API.DTOs.Requests;
using Light_Shop.API.DTOs.Response;
using Light_Shop.API.Models;
using Light_Shop.API.Services.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Light_Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController(IBrandService brandService) : ControllerBase
    {
        private readonly IBrandService _brandService = brandService;

        [HttpGet("")]
        public IActionResult GetAllBrands()
        {
            var brands = _brandService.GetAll();
            if (brands.Count() <= 0) return NotFound(); 
            return Ok(brands.Adapt<IEnumerable<BrandResponse>>());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetBrandById(int id)
        {
            var brand = _brandService.Get(b => b.Id == id);
            if (brand == null) return NotFound();
            return Ok(brand.Adapt<BrandResponse>());
        }

        [HttpPost("")]
        public IActionResult AddBrand(BrandRequest brandRequest)
        {
            var brand = _brandService.Add(brandRequest.Adapt<Brand>());
            return Ok(brand);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateBrand([FromRoute] int id, [FromBody] BrandRequest brandRequest)
        {
            var result = _brandService.Edit(id, brandRequest.Adapt<Brand>());
            if(!result) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteBrand(int id)
        {
            var result = _brandService.Remove(id);
            if(!result) return NotFound();
            return NoContent();
        }
    }
}
