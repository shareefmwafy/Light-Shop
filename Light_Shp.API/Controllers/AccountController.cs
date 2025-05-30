using Light_Shop.API.DTOs.Requests;
using Light_Shop.API.Models;
using Light_Shop.API.Utility;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
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
        private readonly IEmailSender emailSender;
        public AccountController(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender
            ) 
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            var applicationUser = registerRequest.Adapt<ApplicationUser>();
            var result = await userManager.CreateAsync(applicationUser, registerRequest.Password);

            if (result.Succeeded)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Emails", "welcomeMessage.html");

                string welcomeMessage = await System.IO.File.ReadAllTextAsync(filePath);
                await emailSender.SendEmailAsync(applicationUser.Email, "Welcome "+applicationUser.FirstName, welcomeMessage);
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

        [Authorize]
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest changePasswordRequest)
        {
            var applicationUser = await userManager.GetUserAsync(User);
            if (applicationUser != null)
            {
                var result = await userManager.ChangePasswordAsync(applicationUser, changePasswordRequest.OldPassword,
                    changePasswordRequest.NewPassword);
                if (result.Succeeded)
                {
                    return NoContent();
                }

                return BadRequest(result.Errors);
            }
            return BadRequest(new { message = "Invalid Data" });
        }


    }
}
