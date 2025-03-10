using DeepSeekUI.Application.Interfaces.Repositories;
using DeepSeekUI.Domain.Entities;
using DeepSeekUI.Infrastructure.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace DeepSeekUI.Infrastructure.Persistence.Repositories
{
    internal class ChatRepository : GenericRepository<Chat>, IChatRepository
    {
        public ChatRepository(DeepSeekContext context) : base(context)
        {
        }

        public async Task<Chat?> GetChatValidForUser(Guid chatId, Guid userId)
        {
            return await Entities.SingleOrDefaultAsync(c => c.Id == chatId && c.UserId == userId);
        }

        public async Task<Chat?> GetFullChat(Guid ChatId, Guid userId)
        {
            return await Entities.Include(c=>c.Messages)
                .SingleOrDefaultAsync(c =>c.Id == ChatId && c.UserId == userId);
        }
    }
}
