namespace ElegantHome.Data.Models
{
    using System.Collections.Generic;

    using ElegantHome.Data.Common.Models;

    public class Product : BaseDeletableModel<int>
    {
        public Product()
        {
            this.Images = new HashSet<Image>();
            this.WishList = new HashSet<WishList>();
            this.Comments = new List<Comment>();
        }

        public string Name { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string MoreInfo { get; set; }

        public int Quantity { get; set; }

        public string ImageUrl { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<WishList> WishList { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
