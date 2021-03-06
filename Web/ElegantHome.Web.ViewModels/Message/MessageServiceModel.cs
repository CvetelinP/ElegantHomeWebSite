using System;

namespace ElegantHome.Web.ViewModels.Message
{
    public class MessageServiceModel
    {
        public string Id { get; set; }

        public string SenderId { get; set; }

        public string ReceiverId { get; set; }

        public string ConversationId { get; set; }

        public string Text { get; set; }

        public DateTime SendOn { get; set; }
    }
}
