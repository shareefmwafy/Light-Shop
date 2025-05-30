using Light_Shop.API.Models;
using Light_Shop.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Light_Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartsController : ControllerBase
    {
        private readonly ICartService cartService;
        private readonly UserManager<ApplicationUser> userManager;

        public CartsController(ICartService cartService, UserManager<ApplicationUser> userManager)
        {
            this.cartService = cartService;
            this.userManager = userManager;
        }

        [HttpPost("{productId}")]
        public async Task<IActionResult> AddToCart ([FromRoute] int productId, [FromQuery] int count)
        {
            var appUser = userManager.GetUserId(User);
            var Cart = new Cart()
            {
                ProductId = productId,
                Count = count,
                ApplicationUserId = appUser
            };
            await cartService.AddAsync(Cart);
            return Ok(Cart);
        }
    }
}
