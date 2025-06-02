using Light_Shop.API.DTOs.Requests;
using Light_Shop.API.DTOs.Response;
using Light_Shop.API.Models;
using Light_Shop.API.Services.Interfaces;
using Light_Shop.API.Utility;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Light_Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController(ICategoryService categoryService) : ControllerBase
    {
        private readonly ICategoryService _categoryService = categoryService;


        [HttpGet("")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAsync();
            return Ok(categories.Adapt<IEnumerable<CategoryResponse>>());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] int id)
        {
            var category = await _categoryService.GetOneAsync(e => e.Id == id);
            return category == null ? NotFound() : Ok(category.Adapt<CategoryResponse>());
        }

        [HttpPost("")]
        [Authorize(Roles = $"{StaticData.SuperAdmin}, {StaticData.Admin}, {StaticData.Company}")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryRequest categoryRequest, CancellationToken cancellationToken)
        {
            var categoryInDb = await _categoryService.AddAsync(categoryRequest.Adapt<Category>(), cancellationToken);
            return CreatedAtAction(nameof(GetCategoryById), new { categoryInDb.Id }, categoryInDb);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = $"{StaticData.SuperAdmin}, {StaticData.Admin}, {StaticData.Company}")]

        public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] CategoryRequest categoryRequest)
        {
            var categoryInDb = await _categoryService.EditAsync(id, categoryRequest.Adapt<Category>());
            if (!categoryInDb) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = $"{StaticData.SuperAdmin}, {StaticData.Admin}, {StaticData.Company}")]
        public async Task<IActionResult> DeleteCategoryById([FromRoute] int id)
        {
            var categoryInDb = await _categoryService.RemoveAsync(id);
            if (!categoryInDb) return NotFound();
            return NoContent();
        }


    }
}
