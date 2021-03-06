namespace ElegantHome.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ElegantHome.Web.ViewModels.Category;

    public interface ICategoriesService
    {
        Task CreateAsync(CategoryViewModel model);

        IEnumerable<T> GetAll<T>();

        Task DeleteAsync(int id);

        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePairs();

    }
}
