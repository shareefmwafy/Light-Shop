using System.Linq.Expressions;
using Light_Shop.API.Models;

namespace Light_Shop.API.Services.Interfaces
{
    public interface IBrandService
    {
        IEnumerable<Brand> GetAll();
        Brand? Get(Expression <Func<Brand,bool>> expression);
        Brand Add(Brand brand);
        bool Edit(int id, Brand brand);
        bool Remove(int id);
    }
}
