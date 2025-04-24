using Light_Shop.API.Models;
using Light_Shop.API.Services.IService;
using System.Linq.Expressions;
namespace Light_Shop.API.Services.Interfaces
{
    public interface ICategoryService : IService<Category>
    {
        Task<bool> EditAsync(int id, Category category, CancellationToken cancellationToken = default);
        Task<bool> UpdateStatusAsync(int id, Category category, CancellationToken cancellationToken);
    }
}
