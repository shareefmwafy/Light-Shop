using Light_Shop.API.Models;
using System.Linq.Expressions;
using Light_Shop.API.Data;
using Microsoft.EntityFrameworkCore;
using Light_Shop.API.Services.Interfaces;
using System.Threading.Tasks;
using Light_Shop.API.Services.IService;
using Microsoft.AspNetCore.Identity;

namespace Light_Shop.API.Services.Implementations
{
    public class UsersService : Service<ApplicationUser>, IUsersService
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersService(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager
                           
            ):base(context) 
        {
            this._context = context;
            this.userManager = userManager;
        }


        public async Task<bool> ChangeRole(string userId, string roleName)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user is not null)
            {
                var oldRole = await userManager.GetRolesAsync(user);
                await userManager.RemoveFromRolesAsync(user, oldRole);

                var result = await userManager.AddToRoleAsync(user, roleName);

                if (result.Succeeded)
                {
                    return true;
                }
                
            }

            return false;

        }
    }
}
