using DeepSeekUI.Entities.Common;

namespace DeepSeekUI.Domain.Entities
{
    public class Chat : CommonEntity
    {
        private Chat() : base(){ }
        public Chat(string title, Guid userId, List<Message> messages) : base()
        {
            Title = VerifyTitleInput(title);
            LastModified = DateTime.UtcNow;
            UserId = userId;
            _messages = messages ?? new List<Message>();
        }
        public string Title { get; private set; }
        public DateTime LastModified { get; private set; }
        public Guid UserId { get; private set; }
        private List<Message> _messages = new List<Message>();
        public IReadOnlyCollection<Message> Messages => _messages.AsReadOnly();

        public void SetListMessages(List<Message> messages)
        {
            this._messages = messages;
        }
        public void SetTitle(string title)
        {
            this.Title = VerifyTitleInput(title);
        }
        public void SetLastModified()
        {
            this.LastModified = DateTime.UtcNow;
        }

        public Message CreateMessageForChat(string body, MessageType type)
        {
            return Message.Create(body, type, this);
        }

        public void AddMessageToChat(Message messageToAdd)
        {
            this._messages.Add(messageToAdd);
        }

        private string VerifyTitleInput(string title)
        {
            return string.IsNullOrEmpty(title) ? throw new ArgumentException("El atributo {title} no debe ser vacio ni nulo") : title;
        }
    }
}
