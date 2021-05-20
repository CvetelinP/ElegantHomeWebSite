namespace ElegantHome.Data.Models
{
    using System;

    using ElegantHome.Data.Common.Models;

    public class Comment : BaseDeletableModel<string>
    {
        public Comment()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Text { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public DateTime WrittenOn { get; set; }
    }
}
