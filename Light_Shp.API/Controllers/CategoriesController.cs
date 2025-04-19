using Light_Shop.API.DTOs.Requests;
using Light_Shop.API.DTOs.Response;
using Light_Shop.API.Models;
using Light_Shop.API.Services.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Light_Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class CategoriesController(ICategoryService categoryService) : ControllerBase
    {
        private readonly ICategoryService _categoryService = categoryService;


        [HttpGet("")]
        public IActionResult GetAllCategories()
        {
            var categories = _categoryService.GetAll();
            return Ok(categories.Adapt<IEnumerable<CategoryResponse>>());
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoryById([FromRoute] int id)
        {
            var category = _categoryService.Get(e => e.Id == id);
            return category == null ? NotFound() : Ok(category.Adapt<CategoryResponse>());
        }

        [HttpPost("")]
        [Authorize]
        public IActionResult CreateCategory([FromBody] CategoryRequest categoryRequest, CancellationToken cancellationToken)
        {
            var categoryInDb = _categoryService.Add(categoryRequest.Adapt<Category>(), cancellationToken);
            return CreatedAtAction(nameof(GetCategoryById), new { categoryInDb.Id }, categoryInDb);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory([FromRoute] int id, [FromBody] CategoryRequest categoryRequest)
        {
            var categoryInDb = _categoryService.Edit(id, categoryRequest.Adapt<Category>());
            if (!categoryInDb) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategoryById([FromRoute] int id)
        {
            var category = _categoryService.Remove(id);
            if (!category) return NotFound();
            return NoContent();
        }


    }
}
