using Light_Shop.API.Data;
using Light_Shop.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Light_Shop.API.Utility.DBInitilaizer
{
    public class DBInitializer : IDBInitializer
    {
        private readonly ApplicationDbContext context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public DBInitializer(ApplicationDbContext context, 
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager
            )
        {
            this.context = context;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public async Task InitializeAsync()
        {
            try
            {
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            if (roleManager.Roles.IsNullOrEmpty())
            {
                await roleManager.CreateAsync(new(StaticData.SuperAdmin));
                await roleManager.CreateAsync(new(StaticData.Admin));
                await roleManager.CreateAsync(new(StaticData.Company));
                await roleManager.CreateAsync(new(StaticData.Customer));

                await userManager.CreateAsync(new()
                {
                    FirstName = "super",
                    LastName = "admin",
                    UserName = "super_admin",
                    Gender = ApplicationUserGender.Male,
                    BirthOfDate = new DateTime(2002, 05, 16),
                    Email = "admin@lightshop.com"
                }, "Admin@1");

                var user = await userManager.FindByEmailAsync("admin@lightshop.com");

                await userManager.AddToRoleAsync(user, StaticData.SuperAdmin);
            }


        }
    }
}
