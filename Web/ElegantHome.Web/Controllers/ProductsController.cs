namespace ElegantHome.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using ElegantHome.Data.Models;
    using ElegantHome.Services.Data;
    using ElegantHome.Web.ClounaryHelper;
    using ElegantHome.Web.ViewModels.Product;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ProductsController : Controller
    {
        private readonly IProductsService _productService;
        private readonly ICategoriesService _categoriesService;
        private readonly Cloudinary cloudinary;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserAdWishlistService _wishlistService;

        public ProductsController(IProductsService productService, ICategoriesService categoriesService, Cloudinary cloudinary, UserManager<ApplicationUser> userManager, IUserAdWishlistService wishlistService)
        {
            this._productService = productService;
            this._categoriesService = categoriesService;
            this.cloudinary = cloudinary;
            this._userManager = userManager;
            this._wishlistService = wishlistService;
        }

        public IActionResult Add()
        {
            var viewModel = new CreateProductViewModelImages
            {
                CategoriesItems = this._categoriesService.GetAllAsKeyValuePairs(),
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateProductViewModelImages input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoriesItems = this._categoriesService.GetAllAsKeyValuePairs();

                return this.View(input);
            }

            var imageResult = await CloudinaryExtentsion.UploadAsync(this.cloudinary, input.Images);
            var user = await this._userManager.GetUserAsync(this.User);

            await this._productService.CreateAsync(input, imageResult, user.Id);

            return this.RedirectToAction("Add");
        }

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 12;
            var viewModel = new ProductListViewModelWithPaging()
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                ProductsCount = this._productService.GetCount(),
                Products = this._productService.GetAllWithPaging<ProductInListViewModel>(id, ItemsPerPage),
            };
            return this.View(viewModel);
        }

        public async Task<IActionResult> Details(int productId)
        {
            var user = await this._userManager.GetUserAsync(this.User);
            var product = this._productService.ProductProfileInfo(productId);

            var viewModel = new SingleProductViewModel()
            {
                Id = product.Id,
                Description = product.Description,
                CategoryName = product.CategoryName,
                Images = product.Images,
                MoreInfo = product.MoreInfo,
                ImageUrl = product.ImageUrl,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                UserId = product.UserId,
                IsAdInLoggedUserWishlist = this._wishlistService.IsAdInWishlistAsync(this.User.FindFirstValue(ClaimTypes.NameIdentifier), product.Id),
            };

            return this.View(viewModel);
        }

        public IActionResult ByName(int categoryId)
        {
            var viewModel = new ProductListViewModel()
            {
                Products = this._productService.GetByCategoryId<ProductInListViewModel>(categoryId),
            };

            return this.View(viewModel);
        }
    }
}
