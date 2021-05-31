namespace ElegantHome.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ElegantHome.Data.Common.Repositories;
    using ElegantHome.Data.Models;
    using ElegantHome.Services.Mapping;
    using ElegantHome.Web.ViewModels.Category;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task CreateAsync(CategoryViewModel model)
        {
            var category = new Category()
            {
                Name = model.Name,

            };

            await this.categoryRepository.AddAsync(category);
            await this.categoryRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            var category = this.categoryRepository.AllAsNoTracking().To<T>().ToList();

            return category;
        }

        public async Task DeleteAsync(int id)
        {
            var category = this.categoryRepository.All().FirstOrDefault(x => x.Id == id);
            this.categoryRepository.Delete(category);

            await this.categoryRepository.SaveChangesAsync();
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs()
        {
            return this.categoryRepository.AllAsNoTracking()
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                })
                .OrderBy(x => x.Name)
                .ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }
    }
}