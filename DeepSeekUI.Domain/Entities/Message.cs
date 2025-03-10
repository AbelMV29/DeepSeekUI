using DeepSeekUI.Entities.Common;

namespace DeepSeekUI.Domain.Entities
{
    public class Message : CommonEntity
    {
        private Message():base() { }
        private Message(string body, MessageType type, Guid chatId, Chat chat):base()
        {
            Body = string.IsNullOrEmpty(body) ? throw new ArgumentException("El atributo {body} no debe ser vacio ni nulo") : body;
            Type = type;
            ChatId = chatId;
            Chat = chat;
        }

        public string Body { get; private set; }
        public MessageType Type { get; private set; }
        public Guid ChatId { get; private set; }
        public Chat Chat { get; private set; }

        internal static Message Create(string body, MessageType type, Chat chat)
        {
            if (chat is null)
                throw new ArgumentNullException("El argumento {chat} no debe ser nulo");
            return new Message(body, type, chat.Id, chat);
        }
    }

    public enum MessageType
    {
        Question,
        Response
    }
}
