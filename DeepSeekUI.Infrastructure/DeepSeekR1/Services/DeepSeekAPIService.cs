using DeepSeekUI.Application.Dtos;
using DeepSeekUI.Application.Interfaces;
using DeepSeekUI.Infrastructure.DeepSeekR1.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace DeepSeekUI.Infrastructure.DeepSeekR1.Services
{
    public class DeepSeekAPIService : IDeepSeekAPIService
    {
        private readonly IHttpClientFactory _factory;
        public DeepSeekAPIService(IHttpClientFactory factory)
        {
            _factory = factory;
        }
        public async Task<Result<DeepSeekShortResponse>> SendMessage(string message, CancellationToken cancellationToken)
        {
            using HttpClient client = _factory.CreateClient("DeepSeekClient");
            var body = new
            {
                model = "deepseek-r1:8b",
                messages = new[] { new { role = "user", content = message } },
                stream = false
            };

            var bodyContent = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, new MediaTypeHeaderValue("application/json"));
            try
            {
                var response = await client.PostAsync(string.Empty, bodyContent, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    return Result<DeepSeekShortResponse>.Failure("Error al llamar a la API local de DeepSeek");
                }
                var deepSeekResponse = JsonSerializer.Deserialize<DeepSeekResponse>(await response.Content.ReadAsStringAsync());
                string responseFormatted = deepSeekResponse!.Message.Content.Split('>')[2];
                return Result<DeepSeekShortResponse>.Success(new DeepSeekShortResponse(responseFormatted, deepSeekResponse.CreatedAt));
            }
            catch(OperationCanceledException ex)
            {
                return Result<DeepSeekShortResponse>.Failure("Operación cancelada");
            }
            
        }
    }
}
