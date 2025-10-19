using Light_Shop.API.Models;
using Light_Shop.API.Services.IService;
using System.Linq.Expressions;
namespace Light_Shop.API.Services.Interfaces
{
    public interface IUsersService : IService<ApplicationUser>
    {
        Task<bool> ChangeRole(string userId, string roleName);
        Task<bool?> LockUnLock(string userId);

    }
}
