namespace ElegantHome.Data.Models
{
    using ElegantHome.Data.Common.Models;

    public class WishList : BaseDeletableModel<int>
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

    }
}
