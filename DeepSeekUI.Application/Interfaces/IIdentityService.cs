using DeepSeekUI.Application.Dtos;
using DeepSeekUI.Application.Dtos.Identity;

namespace DeepSeekUI.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<AppUserResponse?> GetUserAsync(Guid id);
        Task Logout();
        Task<Result<object>> RegisterAsync(CreateUserRequest request);
        Task<Result<object>> LoginAsync(LoginUserRequest request);
    }
}
