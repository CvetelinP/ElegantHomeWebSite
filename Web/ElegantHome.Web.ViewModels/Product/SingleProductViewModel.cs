namespace ElegantHome.Web.ViewModels.Product
{
    using System.Collections.Generic;

    using ElegantHome.Services.Mapping;

    public class SingleProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string MoreInfo { get; set; }

        public int Quantity { get; set; }

        public string CategoryName { get; set; }

        public string ImageUrl { get; set; }

        public bool IsAdInLoggedUserWishlist { get; set; }

        public string UserId { get; set; }

        public ICollection<ProductImagesViewModel> Images { get; set; }
    }
}
