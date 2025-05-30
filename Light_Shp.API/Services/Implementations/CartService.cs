using Light_Shop.API.Data;
using Light_Shop.API.Models;
using Light_Shop.API.Services.Interfaces;
using Light_Shop.API.Services.IService;
namespace Light_Shop.API.Services.Implementations
{
    public class CartService : Service<Cart>, ICartService
    {
        private readonly ApplicationDbContext _context;

        public CartService(ApplicationDbContext context) : base(context)
        {
            this._context = context;
        }

    }
}
