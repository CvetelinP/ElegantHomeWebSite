namespace ElegantHome.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ElegantHome.Web.ViewModels.Product;

    public interface IProductsService
    {
        Task CreateAsync(CreateProductViewModelImages model, List<string> imagePaths, string userId);

        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAllWithPaging<T>(int page, int itemsPerPage = 12);

        int GetCount();

        SingleProductViewModel ProductProfileInfo(int productId);

        IEnumerable<T> GetByCategoryId<T>(int categoryId);

        Task DeleteAsync(int id);

        IEnumerable<T> Search<T>(string search);
    }
}
