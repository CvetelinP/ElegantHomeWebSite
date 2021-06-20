namespace ElegantHome.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ElegantHome.Data.Common.Repositories;
    using ElegantHome.Data.Models;
    using ElegantHome.Web.ViewModels.Message;
    using Microsoft.EntityFrameworkCore;

    public class MessageService : IMessageService
    {
        private readonly IDeletableEntityRepository<Message> messageRepository;
        private readonly IDeletableEntityRepository<Conversation> conversationRepository;

        public MessageService(IDeletableEntityRepository<Message> messageRepository, IDeletableEntityRepository<Conversation> conversationRepository)
        {
            this.messageRepository = messageRepository;
            this.conversationRepository = conversationRepository;
        }

        public async Task<IEnumerable<MessageServiceModel>> GetAllInConversationAsync(string conversationId)
        {
            return await this.messageRepository.AllAsNoTrackingWithDeleted()
                .Where(m => m.ConversationId == conversationId)
                .OrderBy(m => m.SendOn)
                .Select(m => new MessageServiceModel
                {
                    Id = m.Id,
                    SenderId = m.SenderId,
                    ReceiverId = m.ReceiverId,
                    ConversationId = m.ConversationId,
                    SendOn = m.SendOn,
                    Text = m.Text,
                })
                .ToListAsync();
        }

        public async Task<MessageServiceModel> CreateMessageAsync(string conversationId, string senderId, string receiverId, string text)
        {
            var conversation = this.conversationRepository.AllAsNoTrackingWithDeleted().SingleOrDefault(c => c.Id == conversationId);

            var message = new Message
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                ConversationId = conversationId,
                SendOn = DateTime.UtcNow,
                Text = text,
            };

            await this.messageRepository.AddAsync(message);

            if (conversation.BuyerId == receiverId && conversation.IsReadByBuyer)
            {
                conversation.IsReadByBuyer = false;
                conversation.IsArchivedByBuyer = false;
                this.conversationRepository.Update(conversation);
            }
            else if (conversation.SellerId == receiverId && conversation.IsReadBySeller)
            {
                conversation.IsReadBySeller = false;
                conversation.IsArchivedBySeller = false;
                this.conversationRepository.Update(conversation);
            }

            await this.messageRepository.SaveChangesAsync();

            return new MessageServiceModel
            {
                Id = message.Id,
                SenderId = message.SenderId,
                ReceiverId = message.ReceiverId,
                ConversationId = message.ConversationId,
                SendOn = message.SendOn,
                Text = message.Text,
            };
        }
    }
}
