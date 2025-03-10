namespace DeepSeekUI.Application.Dtos
{
    public class DeepSeekShortResponse
    {
        public string Message { get; set; }
        public DateTime CreatedTime { get; set; }
        public DeepSeekShortResponse()
        {
            
        }
        public DeepSeekShortResponse(string message, DateTime createdTime)
        {
            Message = message;
            CreatedTime = createdTime;

        }
    }
}
