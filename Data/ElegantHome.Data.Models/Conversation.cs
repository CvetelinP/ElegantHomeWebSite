namespace ElegantHome.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ElegantHome.Data.Common.Models;

    public class Conversation : BaseDeletableModel<string>
    {

        public Conversation()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Messages = new HashSet<Message>();
        }

        [Required]
        public string BuyerId { get; set; }

        [Required]
        public string SellerId { get; set; }

        [Required]
        public DateTime StartedOn { get; set; }

        public bool IsReadByBuyer { get; set; }

        public bool IsReadBySeller { get; set; }

        public bool IsArchivedByBuyer { get; set; }

        public bool IsArchivedBySeller { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}
