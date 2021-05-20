using System.Security.Claims;

namespace ElegantHome.Web.Controllers
{
    using System.Threading.Tasks;

    using ElegantHome.Data.Models;
    using ElegantHome.Services.Data;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class UserController : Controller
    {
        private readonly IUserAdWishlistService _wishlistService;
        private readonly IProductsService _productsService;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(IUserAdWishlistService wishlistService, IProductsService productsService, UserManager<ApplicationUser> userManager)
        {
            this._wishlistService = wishlistService;
            this._productsService = productsService;
            this._userManager = userManager;
        }

        public async Task<IActionResult> AddToWishlist(int productId)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var product = this._productsService.ProductProfileInfo(productId);

            await this._wishlistService.AddToWishlistAsync(userId, productId);

            return this.Redirect($"/Products/Details?productId={productId}");
        }

        public async Task<IActionResult> RemoveFromWishlist(int productId)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var product = this._productsService.ProductProfileInfo(productId);

            await this._wishlistService.RemoveFromWishlistAsync(userId, productId);

            return this.Redirect($"/Products/Details?productId={productId}");

        }

    }
}
