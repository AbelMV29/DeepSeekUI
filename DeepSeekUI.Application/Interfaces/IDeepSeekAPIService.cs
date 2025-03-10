using DeepSeekUI.Application.Dtos;

namespace DeepSeekUI.Application.Interfaces
{
    public interface IDeepSeekAPIService
    {
        Task<Result<DeepSeekShortResponse>> SendMessage(string message, CancellationToken cancellationToken);
    }
}
