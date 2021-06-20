using ElegantHome.Data.Common.Models;

namespace ElegantHome.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Message:BaseDeletableModel<string>
    {
        public Message()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string SenderId { get; set; }

        public ApplicationUser Sender { get; set; }

        [Required]
        public string ReceiverId { get; set; }

        public ApplicationUser Receiver { get; set; }

        [Required]
        public DateTime SendOn { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public string ConversationId { get; set; }

        public Conversation Conversation { get; set; }
    }
}
