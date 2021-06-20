namespace ElegantHome.Web.Hubs
{
    using System.Threading.Tasks;

    using ElegantHome.Common;
    using ElegantHome.Data.Models;
    using ElegantHome.Services.Data;
    using ElegantHome.Web.BindingModels.Message;
    using ElegantHome.Web.ViewModels.Message;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.SignalR;

    public class MessageHub:Hub
    {
        private readonly IMessageService messageService;
        private readonly UserManager<ApplicationUser> userManager;

        public MessageHub(IMessageService messageService, UserManager<ApplicationUser> userManager)
        {
            this.messageService = messageService;
            this.userManager = userManager;
        }

        public async Task SendMessage(MessageBindingModel inputModel)
        {
            var messageServiceModel = await messageService.CreateMessageAsync(inputModel.ConversationId, inputModel.SenderId, inputModel.ReceiverId, inputModel.Text);

            var sender = await userManager.FindByIdAsync(messageServiceModel.SenderId);

            var messageViewModel = new MessageViewModel
            {
                SendOn = messageServiceModel.SendOn.ToString(GlobalConstants.DateTimeFormat),
                SenderName = sender.UserName,
                Text = messageServiceModel.Text
            };

            await Clients.Users(inputModel.ReceiverId)
                .SendAsync("SendMessage", messageViewModel);
        }
    }
}
