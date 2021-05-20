namespace ElegantHome.Web.Controllers
{
    using System.Diagnostics;

    using ElegantHome.Services.Data;
    using ElegantHome.Web.ViewModels;
    using ElegantHome.Web.ViewModels.Product;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IProductsService productsService;

        public HomeController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public IActionResult Index()
        {

            var viewModel = new ProductListViewModel()
            {
                Products = this.productsService.GetAll<ProductInListViewModel>(),
            };

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        public IActionResult About()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
