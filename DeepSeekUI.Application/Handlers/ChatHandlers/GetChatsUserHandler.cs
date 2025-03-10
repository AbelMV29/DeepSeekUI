using DeepSeekUI.Application.Dtos;
using DeepSeekUI.Application.Interfaces.Repositories;
using DeepSeekUI.Domain.Entities;

namespace DeepSeekUI.Application.Handlers.ChatHandlers
{
    public class GetChatsUserHandler
    {
        private readonly IChatRepository _repository;

        public GetChatsUserHandler(IChatRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<GetChatsUserResponse[]>> HandleAsync(GetChatsUserRequest request)
        {
            try
            {
                Chat[] chats = _repository.GetAll(chat => chat.UserId == request.UserId).ToArray();

                GetChatsUserResponse[] chatResponse = chats.Select(c =>
                {
                    return new GetChatsUserResponse(c.Id, c.Title, c.LastModified);
                }).ToArray();

                return Result<GetChatsUserResponse[]>.Success(chatResponse);
            }
            catch (Exception ex)
            {
                return Result<GetChatsUserResponse[]>.Failure(ex.Message);
            }
        }
    }
}