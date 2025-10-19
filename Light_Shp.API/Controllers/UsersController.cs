using Light_Shop.API.DTOs.Response;
using Light_Shop.API.Models;
using Light_Shop.API.Services.Interfaces;
using Light_Shop.API.Utility;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Light_Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = $"{StaticData.SuperAdmin}")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var users = await usersService.GetAsync();
            return Ok(users.Adapt<IEnumerable<UserResponse>>());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await usersService.GetOneAsync(user => user.Id == id);
            return Ok(user.Adapt<UserResponse>());
        }


        [HttpGet("{userId}")]
        public async Task<IActionResult> ChangeRole([FromRoute] string userId, [FromQuery] string newRole)
        {
            var result = await usersService.ChangeRole(userId, newRole);
            return Ok(result);
        }

        [HttpPatch("LockUnlock/{userId}")]
        public async Task<IActionResult> LockUnLock(string userId)
        {
            var result = await usersService.LockUnLock(userId);

            if(result == true )
            {
                return Ok(result);
            }
            return BadRequest();
        }

    }
}
