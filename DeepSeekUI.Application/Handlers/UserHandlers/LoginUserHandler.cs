using DeepSeekUI.Application.Dtos;
using DeepSeekUI.Application.Dtos.Identity;
using DeepSeekUI.Application.Interfaces;

namespace DeepSeekUI.Application.Handlers.UserHandlers
{
    public class LoginUserHandler
    {
        private readonly IIdentityService _service;

        public LoginUserHandler(IIdentityService service)
        {
            _service = service;
        }

        public async Task<Result<object>> HandleAsync(LoginUserRequest request)
        {
            return await _service.LoginAsync(request);
        }

        public async Task LogOut()
        {
            await _service.Logout();
        }
    }
}