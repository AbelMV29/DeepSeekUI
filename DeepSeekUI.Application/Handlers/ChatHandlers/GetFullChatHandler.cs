using DeepSeekUI.Application.Dtos;
using DeepSeekUI.Application.Interfaces.Repositories;
using DeepSeekUI.Domain.Entities;

namespace DeepSeekUI.Application.Handlers.ChatHandlers
{
    public class GetFullChatHandler
    {
        private readonly IChatRepository _repository;

        public GetFullChatHandler(IChatRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<GetFullChatResponse>> HandleAsync(GetFullChatRequest request)
        {
            try
            {
                Chat? chat = await _repository.GetFullChat(request.ChatId, request.UserId);

                if(chat is null)
                {
                    return Result<GetFullChatResponse>.Failure("Chat invalido");
                }

                GetFullChatMessageItemResponse[] messagesResponse = chat.Messages.Select(m =>
                {
                    return new GetFullChatMessageItemResponse(m.Body, m.CreatedTime, m.Type);
                }).OrderBy(m=>m.Date).ToArray();

                GetChatsUserResponse chatResponse = new GetChatsUserResponse(chat.Id, chat.Title, chat.LastModified);

                GetFullChatResponse response = new GetFullChatResponse(chatResponse, messagesResponse);

                return Result<GetFullChatResponse>.Success(response);
            }
            catch (Exception ex)
            {
                return Result<GetFullChatResponse>.Failure(ex.Message);
            }
        }
    }
}
