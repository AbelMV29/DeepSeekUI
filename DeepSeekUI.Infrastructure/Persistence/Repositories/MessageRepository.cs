using DeepSeekUI.Application.Interfaces.Repositories;
using DeepSeekUI.Domain.Entities;
using DeepSeekUI.Infrastructure.Persistence.Repositories.Common;

namespace DeepSeekUI.Infrastructure.Persistence.Repositories
{
    internal class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        public MessageRepository(DeepSeekContext context) : base(context)
        {
        }
    }
}
