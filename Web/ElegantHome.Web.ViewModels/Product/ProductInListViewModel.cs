namespace ElegantHome.Web.ViewModels.Product
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using AutoMapper;
    using ElegantHome.Services.Mapping;

    public class ProductInListViewModel : IMapFrom<Data.Models.Product>, IHaveCustomMappings
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

        public string ImageUrl { get; set; }

        public string CategoryName { get; set; }

        // test......................

        public bool IsAdInLoggedUserWishlist { get; set; }

        public string UserUserName { get; set; }

        public string UserId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Data.Models.Product, ProductInListViewModel>()
                .ForMember(x => x.ImageUrl, opt =>
                    opt.MapFrom(x =>
                        x.Images.OrderBy(y => y.CreatedOn).FirstOrDefault().Url));
        }
    }
}
