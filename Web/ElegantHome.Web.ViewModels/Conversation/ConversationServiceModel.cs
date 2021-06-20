using System;

namespace ElegantHome.Web.ViewModels.Conversation
{
    public class ConversationServiceModel
    {
        public string Id { get; set; }

        public string BuyerId { get; set; }

        public string SellerId { get; set; }

        public int ProductId { get; set; }

        public DateTime StartedOn { get; set; }

        public bool IsReadByBuyer { get; set; }

        public bool IsReadBySeller { get; set; }

        public bool IsArchivedByBuyer { get; set; }

        public bool IsArchivedBySeller { get; set; }
    }
}
