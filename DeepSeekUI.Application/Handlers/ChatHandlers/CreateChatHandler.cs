using DeepSeekUI.Application.Dtos;
using DeepSeekUI.Application.Interfaces.Repositories;
using DeepSeekUI.Domain.Entities;

namespace DeepSeekUI.Application.Handlers.ChatHandlers
{
    public class CreateChatHandler
    {
        private readonly IChatRepository _repository;

        public CreateChatHandler(IChatRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<CreateChatResponse>> HandleAsync(CreateChatRequest request)
        {
            Chat newChat = new Chat(request.Title, request.UserId, []);
            try
            {
                Chat chatCreated = await _repository.AddAsync(newChat);
                await _repository.SaveChangesAsync();

                CreateChatResponse response = new(chatCreated.Id, chatCreated.Title, chatCreated.LastModified);
                return Result<CreateChatResponse>.Success(response);
            }
            catch (Exception ex)
            {
                return Result<CreateChatResponse>.Failure(ex.Message);
            }
        }
    }
}