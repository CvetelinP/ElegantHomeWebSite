namespace ElegantHome.Web.Controllers
{
    using System.Threading.Tasks;

    using ElegantHome.Services.Data;
    using ElegantHome.Web.ViewModels.Category;
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.categoriesService.CreateAsync(model);
            return this.RedirectToAction("Add");
        }

        public IActionResult All()
        {
            var viewModel = new CategoryInListViewModel()
            {
                Categories = this.categoriesService.GetAll<CategoryViewModel>(),
            };

            return this.View(viewModel);
        }

    }
}
