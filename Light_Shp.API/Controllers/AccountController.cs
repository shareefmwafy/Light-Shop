using Light_Shop.API.DTOs.Requests;
using Light_Shop.API.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Light_Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) 
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            var applicationUser = registerRequest.Adapt<ApplicationUser>();
            var result = await userManager.CreateAsync(applicationUser, registerRequest.Password);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(applicationUser, false);
                return NoContent();
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async  Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var applicationUser = await userManager.FindByEmailAsync(loginRequest.Email);
            if (applicationUser != null)
            {
                bool result = await userManager.CheckPasswordAsync(applicationUser, loginRequest.Password);
                if (result)
                {
                    await signInManager.SignInAsync(applicationUser, loginRequest.RememberMe);
                    return NoContent();
                }
            }
            return BadRequest(new {message = "Invalid Email or Password"});
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return NoContent();
        }
    }
}
