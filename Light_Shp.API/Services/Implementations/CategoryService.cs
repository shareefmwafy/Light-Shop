using Light_Shop.API.Models;
using System.Linq.Expressions;
using Light_Shop.API.Data;
using Microsoft.EntityFrameworkCore;
using Light_Shop.API.Services.Interfaces;
using System.Threading.Tasks;
using Light_Shop.API.Services.IService;

namespace Light_Shop.API.Services.Implementations
{
    public class CategoryService : Service<Category>, ICategoryService
    {

        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context):base(context) 
        {
            this._context = context;
        }

        public async Task<bool> EditAsync(int id, Category category, CancellationToken cancellationToken = default)
        {
            Category? categoryInDb = _context.Categories.Find(id);
            if (categoryInDb == null) return false;

            categoryInDb.Name = category.Name;
            categoryInDb.Description = category.Description;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> UpdateStatusAsync(int id, Category category, CancellationToken cancellationToken)
        {
            Category? categoryInDb = _context.Categories.Find(id);
            if (categoryInDb == null) return false;

            categoryInDb.Status = !categoryInDb.Status;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
