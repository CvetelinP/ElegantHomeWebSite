namespace ElegantHome.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ElegantHome.Data.Common.Repositories;
    using ElegantHome.Data.Models;
    using ElegantHome.Web.ViewModels.Conversation;
    using Microsoft.EntityFrameworkCore;

    public class ConversationService : IConversationService
    {
        private readonly IDeletableEntityRepository<Conversation> conversationRepository;

        public ConversationService(IDeletableEntityRepository<Conversation> conversationRepository)
        {
            this.conversationRepository = conversationRepository;
        }

        public async Task<ConversationServiceModel> CreateConversationAsync(string buyerId, string sellerId, int productId)
        {
            Conversation conversation = new Conversation
            {
                BuyerId = buyerId,
                SellerId = sellerId,
                ProductId = productId,
                IsReadByBuyer = true,
                IsArchivedBySeller = true,
                IsArchivedByBuyer = false,
                IsReadBySeller = false,
                StartedOn = DateTime.UtcNow,
            };

            await this.conversationRepository.AddAsync(conversation);
            await this.conversationRepository.SaveChangesAsync();

            return new ConversationServiceModel
            {
                Id = conversation.Id,
                BuyerId = conversation.BuyerId,
                SellerId = conversation.SellerId,
                ProductId = conversation.ProductId,
                IsReadByBuyer = conversation.IsReadByBuyer,
                IsReadBySeller = conversation.IsReadBySeller,
                IsArchivedByBuyer = conversation.IsArchivedByBuyer,
                IsArchivedBySeller = conversation.IsArchivedBySeller,
                StartedOn = conversation.StartedOn,
            };
        }

        public async Task<ConversationServiceModel> GetByIdAsync(string conversationId)
        {
            Conversation conversation = await this.conversationRepository.AllAsNoTrackingWithDeleted()
                .FirstOrDefaultAsync(x => x.Id == conversationId);

            return new ConversationServiceModel
            {
                Id = conversation.Id,
                BuyerId = conversation.BuyerId,
                SellerId = conversation.SellerId,
                ProductId = conversation.ProductId,
                IsReadByBuyer = conversation.IsReadByBuyer,
                IsReadBySeller = conversation.IsReadBySeller,
                IsArchivedByBuyer = conversation.IsArchivedByBuyer,
                IsArchivedBySeller = conversation.IsArchivedBySeller,
                StartedOn = conversation.StartedOn,
            };
        }

        public async Task<bool> ConversationExistsAsync(string buyerId, string sellerId, int productId)
        {
            return await this.conversationRepository.AllAsNoTrackingWithDeleted()
                .AnyAsync(c => c.ProductId == productId &&
                               (c.BuyerId == buyerId || c.SellerId == buyerId) &&
                               (c.BuyerId == sellerId || c.SellerId == sellerId));
        }

        public async Task<string> GetIdAsync(string buyerId, string sellerId, int productId)
        {
            if (!await this.ConversationExistsAsync(buyerId, sellerId, productId))
            {
                return null;
            }

            return this.conversationRepository.AllAsNoTrackingWithDeleted().SingleOrDefault(c =>
                c.ProductId == productId && (c.BuyerId == buyerId || c.SellerId == buyerId) &&
                (c.SellerId == buyerId || c.SellerId == sellerId))
                ?.Id;
        }

        public async Task<int> GetAllUnReadByUserIdCountAsync(string userId)
        {
            var conversations = await this.conversationRepository.AllAsNoTracking().Where(c =>
                 (c.BuyerId == userId || c.SellerId == userId)).ToListAsync();

            return conversations.Count;
        }

        public async Task<IEnumerable<ConversationServiceModel>> GetAllByUserIdAsync(string userId)
        {
            return await this.conversationRepository.AllAsNoTracking().Where(c =>
                    (c.BuyerId == userId || c.SellerId == userId))
                .OrderByDescending(c => c.StartedOn)
                .Select(c => new ConversationServiceModel
                {
                    Id = c.Id,
                    BuyerId = c.BuyerId,
                    SellerId = c.SellerId,
                    ProductId = c.ProductId,
                    IsReadByBuyer = c.IsReadByBuyer,
                    IsReadBySeller = c.IsReadBySeller,
                    IsArchivedByBuyer = c.IsArchivedByBuyer,
                    IsArchivedBySeller = c.IsArchivedBySeller,
                    StartedOn = c.StartedOn,
                })
                .ToListAsync();
        }

        public async Task<bool> MarkConversationAsReadAsync(string conversationId, string userId)
        {
            if (!await this.conversationRepository.All().AnyAsync(c => c.Id == conversationId))
            {
                return false;
            }

            var conversation = await this.conversationRepository.All().SingleOrDefaultAsync(c => c.Id == conversationId);

            if (conversation.BuyerId == userId)
            {
                conversation.IsReadByBuyer = true;
            }
            else if (conversation.SellerId == userId)
            {
                conversation.IsReadBySeller = true;
            }
            else
            {
                return false;
            }

            this.conversationRepository.Update(conversation);
            await this.conversationRepository.SaveChangesAsync();

            return true;
        }
    }
}
