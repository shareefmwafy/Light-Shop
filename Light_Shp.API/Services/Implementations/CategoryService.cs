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
        public async Task<Category> Add(Category category, CancellationToken cancellationToken = default)
        {
            await context.Categories.AddAsync(category, cancellationToken);
            await context.SaveChangesAsync();
            return category;
        }

        public bool Edit(int id, Category category)
        {
            Category? categoryInDb = context.Categories.AsNoTracking().FirstOrDefault(c => c.Id == id);
            if (categoryInDb == null) return false;

            category.Id = id;
            context.Categories.Update(category);
            context.SaveChanges();
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

        public bool Remove(int id)
        {
            Category? categoryInDb = context.Categories.Find(id);
            if (categoryInDb == null) return false;

            context.Categories.Remove(categoryInDb);
            context.SaveChanges();
            return true;
        }
    }
}
