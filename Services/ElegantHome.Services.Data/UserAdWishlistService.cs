using System.Collections.Generic;
using ElegantHome.Web.ViewModels.Product;
using ElegantHome.Web.ViewModels.WishList;

namespace ElegantHome.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using ElegantHome.Data.Common.Repositories;
    using ElegantHome.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class UserAdWishlistService : IUserAdWishlistService
    {
        private readonly IDeletableEntityRepository<WishList> _wishRepository;

        public UserAdWishlistService(IDeletableEntityRepository<WishList> wishRepository)
        {
            this._wishRepository = wishRepository;
        }

        public async Task AddToWishlistAsync(string userId, int productId)
        {
            await this._wishRepository.AddAsync(new WishList
            {
                UserId = userId,
                ProductId = productId,
            });

            await this._wishRepository.SaveChangesAsync();
        }

        public async Task RemoveFromWishlistAsync(string userId, int productId)
        {
            var model = this._wishRepository.All().FirstOrDefault(x => x.ProductId == productId && x.UserId == userId);

            this._wishRepository.Delete(model);

            await this._wishRepository.SaveChangesAsync();
        }

        public bool IsAdInWishlistAsync(string userId, int productId)
        {
            return this._wishRepository.All().Any(x => x.ProductId == productId && x.UserId == userId);
        }

        public async Task<IEnumerable<SingleProductViewModel>> GetUserWishlistAsync(string userId)
        {
            var wishList = await this._wishRepository.All()
                .Where(x => x.UserId == userId).Select(w => w.Product)
                .ToListAsync();

            var result = wishList.Select(x => new SingleProductViewModel()
            {
                Name = x.Name,
                Price = x.Price,
                Id = x.Id,
            });

            return result;
        }
    }
}
