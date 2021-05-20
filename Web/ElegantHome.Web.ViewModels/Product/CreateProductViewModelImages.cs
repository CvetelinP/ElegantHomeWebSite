namespace ElegantHome.Web.ViewModels.Product
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Http;

    public class CreateProductViewModelImages : ProductViewModel
    {
        public IEnumerable<IFormFile> Images { get; set; }
    }
}
