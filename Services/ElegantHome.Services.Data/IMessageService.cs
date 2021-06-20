namespace ElegantHome.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ElegantHome.Web.ViewModels.Message;

    public interface IMessageService
    {
        Task<IEnumerable<MessageServiceModel>> GetAllInConversationAsync(string conversationId);

        Task<MessageServiceModel> CreateMessageAsync(string conversationId, string senderId, string receiverId, string text);
    }
}
