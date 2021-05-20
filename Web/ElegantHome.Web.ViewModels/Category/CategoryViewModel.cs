namespace ElegantHome.Web.ViewModels.Category
{
    using System.ComponentModel.DataAnnotations;

    using ElegantHome.Services.Mapping;

    public class CategoryViewModel : IMapFrom<Data.Models.Category>
    {

        [Required]
        [MinLength(3)]
        public string Name { get; set; }
    }
}
