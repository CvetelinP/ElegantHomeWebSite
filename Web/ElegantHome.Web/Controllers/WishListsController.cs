namespace ElegantHome.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using ElegantHome.Services.Data;
    using ElegantHome.Web.ViewModels.WishList;
    using Microsoft.AspNetCore.Mvc;

    public class WishListsController : Controller
    {
        private readonly IUserAdWishlistService _wishlistService;
        private readonly IProductsService productService;

        public WishListsController(IUserAdWishlistService wishlistService, IProductsService productService)
        {
            this._wishlistService = wishlistService;
            this.productService = productService;
        }

        public async Task<IActionResult> All()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var wishList = await this._wishlistService.GetUserWishlistAsync(userId);

            var viewModel = new WishlistViewModel();

            foreach (var wish in wishList)
            {
                viewModel.WishLists.Add(new WishlistAdViewModel
                {
                    Id = wish.Id,
                    Name = wish.Name,
                    Price = wish.Price,
                });
            }

            return this.View(viewModel);
        }
    }
}
