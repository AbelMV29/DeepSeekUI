using DeepSeekUI.Application.Dtos;
using DeepSeekUI.Application.Dtos.Identity;
using DeepSeekUI.Application.Interfaces;

namespace DeepSeekUI.Application.Handlers.UserHandlers
{
    public class CreateUserHandler
    {
        private readonly IIdentityService _service;

        public CreateUserHandler(IIdentityService service)
        {
            _service = service;
        }

        public async Task<Result<object>> HandleAsync(CreateUserRequest request)
        {
            return await _service.RegisterAsync(request);
        }
    }
}