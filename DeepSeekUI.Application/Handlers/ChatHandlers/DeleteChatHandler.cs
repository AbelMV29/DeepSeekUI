using DeepSeekUI.Application.Dtos;
using DeepSeekUI.Application.Interfaces.Repositories;
using DeepSeekUI.Domain.Entities;

namespace DeepSeekUI.Application.Handlers.ChatHandlers
{
    public class DeleteChatHandler
    {
        private readonly IChatRepository _repository;

        public DeleteChatHandler(IChatRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<object>> HandleAsync(DeleteChatRequest request)
        {
            try
            {
                Chat? chatToDeleted = await _repository.GetChatValidForUser(request.ChatId, request.UserId);
                if(chatToDeleted is null)
                    return Result<object>.Failure("Chat inexistente");
                await _repository.Delete(chatToDeleted!);
                await _repository.SaveChangesAsync();

                return Result<object>.Success(null, message: "Chat eliminado con exito");
            }
            catch (Exception ex)
            {
                return Result<object>.Failure(ex.Message);
            }
        }
    }
}
