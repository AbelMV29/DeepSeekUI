using DeepSeekUI.Application.Interfaces.Repositories.Common;
using DeepSeekUI.Domain.Entities;

namespace DeepSeekUI.Application.Interfaces.Repositories
{
    public interface IChatRepository : IGenericRepository<Chat>
    {
        public Task<Chat?> GetChatValidForUser(Guid chatId, Guid userId);
        public Task<Chat?> GetFullChat(Guid ChatId, Guid userId);
    }
}
