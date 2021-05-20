namespace ElegantHome.Web.ViewModels.Product
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ElegantHome.Services.Mapping;

    public class ProductViewModel : IMapFrom<Data.Models.Product>
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(400)]
        public string Description { get; set; }

        public string MoreInfo { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public string UserId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CategoriesItems { get; set; }

    }
}
