namespace ElegantHome.Web.ViewComponents
{
    using ElegantHome.Data.Models;
    using ElegantHome.Web.BindingModels.Message;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class SendMessageViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> userManager;

        public SendMessageViewComponent(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public IViewComponentResult Invoke(string receiverId, string senderId, string conversationId)
        {
            var inputModel = new MessageBindingModel
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                ConversationId = conversationId,
            };

            return this.View(inputModel);
        }
    }
}
