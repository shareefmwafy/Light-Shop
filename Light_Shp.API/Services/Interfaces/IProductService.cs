using Light_Shop.API.DTOs.Requests;
using Light_Shop.API.Models;
using System.Linq.Expressions;

namespace Light_Shop.API.Services.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll(string? query, int page, int limit);
        Product? Get(Expression<Func<Product, bool>> expression);
        Product Add(ProductRequest productRequest);
        bool Edit(int id, ProductUpdateRequest productRequest);
        bool Remove(int id);
    }
}
