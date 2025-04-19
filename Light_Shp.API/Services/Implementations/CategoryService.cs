using Light_Shop.API.Models;
using System.Linq.Expressions;
using Light_Shop.API.Data;
using Microsoft.EntityFrameworkCore;
using Light_Shop.API.Services.Interfaces;
using System.Threading.Tasks;

namespace Light_Shop.API.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        ApplicationDbContext context;
        public CategoryService(ApplicationDbContext context) 
        {
            this.context = context;
        }
        public async Task<Category> AddAsync(Category category, CancellationToken cancellationToken = default)
        {
            await context.Categories.AddAsync(category, cancellationToken);
            await context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> EditAsync(int id, Category category, CancellationToken cancellationToken)
        {
            Category? categoryInDb = context.Categories.Find(id);
            if (categoryInDb == null) return false;
            categoryInDb.Name = category.Name;
            categoryInDb.Description = category.Description;
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
        public async Task<bool> UpdateStatusAsync(int id, Category category, CancellationToken cancellationToken)
        {
            Category? categoryInDb = context.Categories.Find(id);
            if (categoryInDb == null) return false;
            categoryInDb.Status = !categoryInDb.Status;
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public Category? Get(Expression<Func<Category, bool>> expression)
        {
            return context.Categories.FirstOrDefault(expression);
        }

        public IEnumerable<Category> GetAll()
        {
            return context.Categories.ToList();
        }

        public async Task<bool> RemoveAsync(int id, CancellationToken cancellationToken)
        {
            Category? categoryInDb = context.Categories.Find(id);
            if (categoryInDb == null) return false;

            context.Categories.Remove(categoryInDb);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
