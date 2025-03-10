using System.Text.Json.Serialization;

namespace DeepSeekUI.Infrastructure.DeepSeekR1.Models
{
    public record DeepSeekResponse
    {
        public string Model { get; set; }
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("message")]
        public DeepSeekMessageResponse Message { get; set; }
        [JsonPropertyName("done_reason")]
        public string DoneReason { get; set; }
        public bool Done { get; set; }
        [JsonPropertyName("total_duration")]
        public long TotalDuration { get; set; }
        [JsonPropertyName("load_duration")]
        public long LoadDuration { get; set; }
        [JsonPropertyName("prompt_eval_count")]
        public int PromptEvalCount { get; set; }
        [JsonPropertyName("prompt_eval_duration")]
        public long PromptEvalDuration { get; set; }
        [JsonPropertyName("eval_count")]
        public int EvalCount { get; set; }
        [JsonPropertyName("eval_duration")]
        public long EvalDuration { get; set; }
    }

    public record DeepSeekMessageResponse
    {
        [JsonPropertyName("role")]
        public string Role { get; set; }
        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
}
