using Light_Shop.API.DTOs.Requests;
using Light_Shop.API.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Light_Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        public AccountController(UserManager<ApplicationUser> userManager) 
        {
            this.userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            var applicationUser = registerRequest.Adapt<ApplicationUser>();
            var result = await userManager.CreateAsync(applicationUser, registerRequest.Password);
            if (result.Succeeded)
            {
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
                    return NoContent();
                }
            }
            return BadRequest(new {message = "Invalid Email or Password"});
        }
    }
}
