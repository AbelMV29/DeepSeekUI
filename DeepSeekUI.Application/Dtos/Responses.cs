using DeepSeekUI.Domain.Entities;

namespace DeepSeekUI.Application.Dtos
{
    public record GetChatsUserResponse(Guid ChatId, string Title, DateTime LastModified);
    public record GetFullChatResponse(GetChatsUserResponse Chat, GetFullChatMessageItemResponse[] Messages);
    public record GetFullChatMessageItemResponse(string Message, DateTime Date, MessageType MessageType);
    public record CreateChatResponse(Guid Id, string Title, DateTime LastModified);
}
