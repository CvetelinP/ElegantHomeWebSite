namespace ElegantHome.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using ElegantHome.Common;
    using ElegantHome.Data.Models;
    using ElegantHome.Services.Data;
    using ElegantHome.Web.BindingModels.Message;
    using ElegantHome.Web.Hubs;
    using ElegantHome.Web.ViewModels.Message;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;

    public class MessageController : Controller
    {
        private readonly IConversationService conversationService;
        private readonly IProductsService productService;
        private readonly IMessageService messageService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHubContext<MessageHub> hubContext;

        public MessageController(IConversationService conversationService,
            IProductsService productService,
            IMessageService messageService,
            UserManager<ApplicationUser>userManager,
            IHubContext<MessageHub> hubContext)
        {
            this.conversationService = conversationService;
            this.productService = productService;
            this.messageService = messageService;
            this.userManager = userManager;
            this.hubContext = hubContext;
        }

        public async Task<IActionResult> Chat(string conversationId)
        {
            var conversation = await this.conversationService.GetByIdAsync(conversationId);
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await this.conversationService.MarkConversationAsReadAsync(conversation.Id, userId);
            var product = this.productService.ProductProfileInfo(conversation.ProductId);

            var messages = await this.messageService.GetAllInConversationAsync(conversationId);

            var onMessageSendReceiverId = "";

            if (conversation.BuyerId == userId)
            {
                onMessageSendReceiverId = conversation.SellerId;
            }
            else
            {
                onMessageSendReceiverId = conversation.BuyerId;
            }

            var viewModel = new MessagesChatViewModel
            {
                ProductId = product.Id,
                ProductName = product.Name,
                ConversationId = conversation.Id,
                OnMessageSendSenderId = userId,
                OnMessageSendReceiverId = onMessageSendReceiverId,
            };
            foreach (var message in messages)
            {
                var sender = await this.userManager.FindByIdAsync(message.SenderId);

                viewModel.Messages.Add(new MessageViewModel
                {
                    SenderName = sender.UserName,
                    SendOn = message.SendOn.ToString(GlobalConstants.DateTimeFormat),
                    Text = message.Text,
                });
            }

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Send(MessageBindingModel inputModel)
        {
            var message = await messageService.CreateMessageAsync(inputModel.ConversationId, inputModel.SenderId,
                inputModel.ReceiverId, inputModel.Text);

            var sender = await this.userManager.FindByIdAsync(message.SenderId);

            var messageViewModel = new MessageViewModel
            {
                Text = message.Text,
                SenderName = sender.UserName,
                SendOn = message.SendOn.ToString(GlobalConstants.DateTimeFormat),
            };

            await this.hubContext.Clients.User(inputModel.ReceiverId)
                .SendAsync("SendMessage", messageViewModel);

            return this.Json(message);
        }
    }
}
