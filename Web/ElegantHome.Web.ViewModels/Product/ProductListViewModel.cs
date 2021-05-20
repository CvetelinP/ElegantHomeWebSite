namespace ElegantHome.Web.ViewModels.Product
{
    using System.Collections.Generic;

    public class ProductListViewModel
    {
        public IEnumerable<ProductInListViewModel> Products { get; set; }
    }
}
