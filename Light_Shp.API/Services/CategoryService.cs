using Light_Shop.API.Models;
using System.Linq.Expressions;
using Light_Shop.API.Data;
using Microsoft.EntityFrameworkCore;
namespace Light_Shop.API.Services
{
    public class CategoryService : ICategoryService
    {
        ApplicationDbContext context;
        public CategoryService(ApplicationDbContext context) 
        {
            this.context = context;
        }
        public Category Add(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
            return category;
        }

        public bool Edit(int id, Category category)
        {
            Category? categoryInDb = context.Categories.AsNoTracking().FirstOrDefault(c => c.Id == id);
            if (categoryInDb == null) return false;

            categoryInDb.Id = id;
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
