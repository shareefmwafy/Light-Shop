using Light_Shop.API.Models;
using System.Linq.Expressions;

namespace Light_Shop.API.Services.IService
{
    public interface IService<T> where T : class
    {
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>>? expression = null, Expression<Func<T, object>>?[] include = null, bool isTracked = true);
        Task <T?> GetOneAsync(Expression<Func<T, bool>> expression = null, Expression<Func<T, object>>?[] include = null, bool isTracked = true);
        Task<T> AddAsync(T T, CancellationToken cancellationToken = default);
        Task<bool> RemoveAsync(int id, CancellationToken cancellationToken = default);
    }
}
