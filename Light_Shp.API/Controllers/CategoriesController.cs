using Light_Shop.API.Data;
using Light_Shop.API.DTOs.Requests;
using Light_Shop.API.DTOs.Response;
using Light_Shop.API.Models;
using Light_Shop.API.Services.CategoryServices;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Light_Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ICategoryService categoryService) : ControllerBase
    {
        ICategoryService categoryService = categoryService;


        [HttpGet("")]
        public IActionResult getAllCategories()
        {
            var categories = categoryService.GetAll();
            return Ok(categories.Adapt<IEnumerable<CategoryResponse>>());
        }

        [HttpGet("{id}")]
        public IActionResult getCategoryById([FromRoute] int id)
        {
            var category = categoryService.Get(e => e.Id == id);
            return category == null ? NotFound() : Ok(category.Adapt<CategoryResponse>());
        }

        [HttpPost("")]
        public IActionResult createCategory([FromBody] CategoryRequest categoryRequest)
        {
            var categoryInDb = categoryService.Add(categoryRequest.Adapt<Category>());
            return CreatedAtAction(nameof(getCategoryById), new { categoryInDb.Id }, categoryInDb);
        }

        [HttpPut("{id}")]
        public IActionResult updateCategory([FromRoute] int id, [FromBody] CategoryRequest categoryRequest)
        {
            var categoryInDb = categoryService.Edit(id, categoryRequest.Adapt<Category>());
            if (!categoryInDb) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult deleteCategoryById([FromRoute] int id)
        {
            var category = categoryService.Remove(id);
            if (!category) return NotFound();
            return NoContent();
        }


    }
}
