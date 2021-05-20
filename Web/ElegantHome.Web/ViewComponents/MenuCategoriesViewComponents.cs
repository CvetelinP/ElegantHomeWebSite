namespace ElegantHome.Web.ViewComponents
{
    using System.Linq;

    using ElegantHome.Data.Common.Repositories;
    using ElegantHome.Data.Models;
    using ElegantHome.Services.Mapping;
    using ElegantHome.Web.ViewModels.Category;
    using Microsoft.AspNetCore.Mvc;

    public class MenuCategoriesViewComponents : ViewComponent
    {
        private readonly IDeletableEntityRepository<Category> _categoryRepository;

        public MenuCategoriesViewComponents(IDeletableEntityRepository<Category> categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        public IViewComponentResult Invoke()
        {
            var model = new CategoryInListViewModel
            {
                Categories = this._categoryRepository.All().To<CategoryViewModel>().ToList(),
            };

            return this.View(model);

        }
    }
}
