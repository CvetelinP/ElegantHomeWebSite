namespace ElegantHome.Web.ViewModels.Product
{
    using System.Collections.Generic;

    using ElegantHome.Services.Mapping;

    public class ProductListViewModel
    {
        public IEnumerable<ProductInListViewModel> Products { get; set; }
    }
}
