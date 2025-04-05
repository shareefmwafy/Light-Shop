using Light_Shop.API.DTOs.Requests;
using Light_Shop.API.Models;
using System.Linq.Expressions;

namespace Light_Shop.API.Services.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product? Get(Expression<Func<Product, bool>> expression);
        Product Add(ProductRequest productRequest);
        bool Edit(int id, ProductRequest productRequest);
        bool Remove(int id);
    }
}
