using Light_Shop.API.Data;
using Light_Shop.API.Models;
using Light_Shop.API.Services;
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
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult getCategoryById([FromRoute] int id)
        {
            var category = categoryService.Get(e => e.Id == id);
            return category == null ? NotFound() : Ok(category);
        }

        [HttpPost("")]
        public IActionResult createCategory([FromBody] Category category)
        {
            var categoryInDb = categoryService.Add(category);
            return CreatedAtAction(nameof(getCategoryById), new { categoryInDb.Id}, categoryInDb);        
        }

        [HttpPut("{id}")]
        public IActionResult updateCategory([FromRoute] int id, [FromBody] Category category)
        {
            // I use AsNoTracking() to improve performance and prevent tracking conflicts during update
            var categoryInDb = categoryService.Edit(id,category);  
            if (!categoryInDb) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult deleteCategoryById([FromRoute] int id)
        {
            var category = categoryService.Remove(id);
            if(!category) return NotFound();
            return NoContent();
        }


    }
}
