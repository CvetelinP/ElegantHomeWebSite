using System.Security.Claims;

namespace ElegantHome.Web.Controllers
{
    using System.Threading.Tasks;

    using ElegantHome.Services.Data;
    using Microsoft.AspNetCore.Mvc;

    public class ConversationController : Controller
    {
        private readonly IConversationService conversationService;

        public ConversationController(IConversationService conversationService)
        {
            this.conversationService = conversationService;
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
    }
}
