using Light_Shop.API.Models;
using System.Linq.Expressions;
namespace Light_Shop.API.Services
{
    public interface ICategoryService
    {
        // I use IEnumerable to use categories as multi types of collections
        IEnumerable<Category> GetAll();
        Category? Get(Expression <Func<Category,bool>> expression);
        Category Add(Category category);
        bool Edit(int id, Category category);
        bool Remove(int id);
    }
}
