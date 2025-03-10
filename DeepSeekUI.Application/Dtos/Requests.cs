using System.ComponentModel.DataAnnotations;

namespace DeepSeekUI.Application.Dtos
{
    public record GetUserRequest
    {
        public GetUserRequest(Guid id)
        {
            Id = id;
        }

        [Required]
        public Guid Id { get; init; }
    }
    public class SendMessageRequest
    {
        [Required(ErrorMessage = "El mensaje es requerido")]
        public string Message { get; set; }
        public Guid? ChatId { get; set; }
        public Guid? UserId { get; set; }
    }
    public class CreateChatRequest
    {
        public string Title { get; set; }
        public Guid UserId { get; set; }
    }
    public record GetChatsUserRequest(Guid UserId);

    public record GetFullChatRequest(Guid ChatId, Guid UserId);
    public record DeleteChatRequest(Guid ChatId, Guid UserId);
}
