using Light_Shop.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Light_Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        ApplicationDbContext context;

        CategoriesController(ApplicationDbContext context)
        {
            this.context = context;
        }

       

    }
}
