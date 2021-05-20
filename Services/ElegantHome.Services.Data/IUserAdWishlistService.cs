using ElegantHome.Web.ViewModels.WishList;

namespace ElegantHome.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ElegantHome.Web.ViewModels.Product;

    public interface IUserAdWishlistService
    {
        Task AddToWishlistAsync(string userId, int productId);

        Task RemoveFromWishlistAsync(string userId, int productId);

        bool IsAdInWishlistAsync(string userId, int productId);

        Task<IEnumerable<SingleProductViewModel>> GetUserWishlistAsync(string userId);
    }
}
