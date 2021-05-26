using Microsoft.AspNetCore.Identity;

namespace ElegantHome.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ElegantHome.Data.Common.Repositories;
    using ElegantHome.Data.Models;
    using ElegantHome.Services.Mapping;
    using ElegantHome.Web.ViewModels.Product;

    public class ProductService : IProductsService
    {
        private readonly IDeletableEntityRepository<Product> productRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDeletableEntityRepository<WishList> _wishRepository;

        public ProductService(IDeletableEntityRepository<Product> productRepository, UserManager<ApplicationUser> userManager, IDeletableEntityRepository<WishList> wishRepository)
        {
            this.productRepository = productRepository;
            this._userManager = userManager;
            this._wishRepository = wishRepository;
        }

        public async Task CreateAsync(CreateProductViewModelImages model, List<string> imagePaths, string userId)
        {
            var images = new List<Image>();

            foreach (var imagePath in imagePaths)
            {
                var image = new Image()
                {
                    Url = imagePath,
                    ProductId = model.Id,
                };
                images.Add(image);
            }

            var product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                MoreInfo = model.MoreInfo,
                Price = model.Price,
                Quantity = model.Quantity,
                CategoryId = model.CategoryId,
                Images = images,
                UserId = userId,
            };

            await this.productRepository.AddAsync(product);
            await this.productRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            var product = this.productRepository.AllAsNoTracking()
                .To<T>()
                .ToList();

            return product;
        }

        public IEnumerable<T> GetAllWithPaging<T>(int page, int itemsPerPage = 12)
        {
            var productWithPaging = this.productRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>().ToList();
            return productWithPaging;
        }

        public int GetCount()
        {
            return this.productRepository.All().Count();
        }

        public SingleProductViewModel ProductProfileInfo(int productId)
        {
            return this.productRepository.AllAsNoTracking().Where(x => x.Id == productId).Select(x => new SingleProductViewModel()
            {
                Name = x.Name,
                Description = x.Description ?? string.Empty,
                MoreInfo = x.MoreInfo,
                Price = x.Price,
                Id = x.Id,
                UserId = x.UserId,
                Images = x.Images.Select(x => new ProductImagesViewModel() { ImageUrl = x.Url, Id = x.Id, CreatedOn = x.CreatedOn }).OrderByDescending(x => x.CreatedOn).ToList(),
                ImageUrl = x.Images.OrderBy(x => x.CreatedOn).FirstOrDefault().Url,
                CategoryName = x.Category.Name,
            }).FirstOrDefault();
        }

        public IEnumerable<T> GetByCategoryId<T>(int categoryId)
        {
            var query = this.productRepository.All()
                .Where(x => x.CategoryId == categoryId).To<T>().ToList();

            return query;
        }

        public async Task DeleteAsync(int id)
        {
            var product = this.productRepository.All().FirstOrDefault(x => x.Id == id);

            this.productRepository.Delete(product);

            await this.productRepository.SaveChangesAsync();
        }
    }
}
