namespace ElegantHome.Web.Controllers
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using ElegantHome.Common;
    using ElegantHome.Data.Models;
    using ElegantHome.Services.Data;
    using ElegantHome.Web.ViewModels.Conversation;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ConversationController : Controller
    {
        private readonly IConversationService conversationService;
        private readonly IProductsService productsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ConversationController(IConversationService conversationService, IProductsService productsService,UserManager<ApplicationUser>userManager)
        {
            this.conversationService = conversationService;
            this.productsService = productsService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Create(string buyerId, string sellerId, int productId)
        {
            if (await this.conversationService.ConversationExistsAsync(buyerId, sellerId, productId))
            {
                var id = await this.conversationService.GetIdAsync(buyerId, sellerId, productId);

                return this.Redirect($"/Message/Chat?conversationId={id}");
            }

            var conversation = await this.conversationService.CreateConversationAsync(buyerId, sellerId, productId);

            return this.Redirect($"/Message/Chat?conversationId={conversation.Id}");
        }

        public async Task<IActionResult> GetAllCount(string userId)
        {
            var user = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var conversationsCount = await this.conversationService.GetAllUnReadByUserIdCountAsync(user);

            return this.Json(conversationsCount);
        }

        public async Task<IActionResult> All()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var conversations = await this.conversationService.GetAllByUserIdAsync(userId);

            var viewModel = new List<ConversationViewModel>();

            foreach (var conversation in conversations)
            {
                var product = this.productsService.ProductProfileInfo(conversation.ProductId);

                var buyer = await this.userManager.FindByIdAsync(conversation.BuyerId);
                var seller = await this.userManager.FindByIdAsync(conversation.SellerId);

                var conversationViewModel = new ConversationViewModel
                {
                    Id = conversation.Id,
                    BuyerId = conversation.BuyerId,
                    SellerId = conversation.SellerId,
                    BuyerName = buyer.UserName,
                    SellerName = seller.UserName,
                    ProductId = product.Id,
                    ProductName = product.Name,
                    StartedOn = conversation.StartedOn.ToString(GlobalConstants.DateTimeFormat),
                };

                if (product.UserId == userId && userId == conversation.SellerId)
                {
                    conversationViewModel.IsRead = conversation.IsReadBySeller;
                }
                else
                {
                    conversationViewModel.IsRead = conversation.IsReadByBuyer;
                }

                viewModel.Add(conversationViewModel);
            }

            return this.View(viewModel);
        }
    }
}
