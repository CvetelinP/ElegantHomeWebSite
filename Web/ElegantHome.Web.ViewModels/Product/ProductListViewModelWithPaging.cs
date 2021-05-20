namespace ElegantHome.Web.ViewModels.Product
{
    using System.Collections.Generic;

    using ElegantHome.Web.ViewModels.Paging;

    public class ProductListViewModelWithPaging : PagingViewModel
    {
        public IEnumerable<ProductInListViewModel> Products { get; set; }
    }
}
