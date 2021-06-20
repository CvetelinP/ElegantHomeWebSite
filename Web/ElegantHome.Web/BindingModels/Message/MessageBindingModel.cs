namespace ElegantHome.Web.BindingModels.Message
{
    using System.ComponentModel.DataAnnotations;

    public class MessageBindingModel
    {
        [Required]
        public string Text { get; set; }

        [Required]
        public string SenderId { get; set; }

        [Required]
        public string ReceiverId { get; set; }

        [Required]
        public string ConversationId { get; set; }
    }
}
