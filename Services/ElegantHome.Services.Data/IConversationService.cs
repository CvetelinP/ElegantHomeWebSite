namespace ElegantHome.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ElegantHome.Web.ViewModels.Conversation;

    public interface IConversationService
    {
        Task<ConversationServiceModel> CreateConversationAsync(string buyerId, string sellerId, int productId);

        Task<ConversationServiceModel> GetByIdAsync(string conversationId);

        Task<bool> ConversationExistsAsync(string buyerId, string sellerId, int productId);

        Task<string> GetIdAsync(string buyerId, string sellerId, int productId);

        Task<int> GetAllUnReadByUserIdCountAsync(string userId);

        Task<IEnumerable<ConversationServiceModel>> GetAllByUserIdAsync(string userId);
    }
}
