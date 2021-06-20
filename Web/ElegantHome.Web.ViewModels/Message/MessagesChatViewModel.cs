using System.Collections.Generic;

namespace ElegantHome.Web.ViewModels.Message
{
    public class MessagesChatViewModel
    {
        public string ProductName { get; set; }

        public int ProductId { get; set; }

        public List<MessageViewModel> Messages { get; set; } = new List<MessageViewModel>();

        public string OnMessageSendSenderId { get; set; }

        public string OnMessageSendReceiverId { get; set; }

        public string ConversationId { get; set; }
    }
}
