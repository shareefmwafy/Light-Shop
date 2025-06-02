using Light_Shop.API.DTOs.Requests;
using Light_Shop.API.Models;
using Light_Shop.API.Utility;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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

                await userManager.AddToRoleAsync(applicationUser, StaticData.Customer);
                    
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
                List<Claim> claims = new();
                claims.Add(new(ClaimTypes.Name,applicationUser.UserName));
                var userRoles = await userManager.GetRolesAsync(applicationUser);
                claims.Add(new("id",applicationUser.Id));
                if(userRoles.Count > 0)
                {
                    foreach (var role in userRoles)
                    {
                        claims.Add(new(ClaimTypes.Role, role));
                    }
                }

                if (result)
                {
                    SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("tstNdVvKmZmOQlahHTbWaSSoBKfSTfds"));
                    SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey,SecurityAlgorithms.HmacSha256);
                    var JwtToken = new JwtSecurityToken(
                        expires: DateTime.Now.AddDays(1),
                        claims: claims,
                        signingCredentials: signingCredentials
                        );

                    string token = new JwtSecurityTokenHandler().WriteToken(JwtToken);
                    return Ok(new { token });
                }
            }
            return BadRequest(new {message = "Invalid Email or Password"});
        }

        [HttpGet("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return NoContent();
        }

        [HttpPost("ChangePassword")]
        [Authorize]
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
