namespace ElegantHome.Data.Models
{
    using System;

    using ElegantHome.Data.Common.Models;

    public class Image : BaseModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public string Url { get; set; }
    }
}
