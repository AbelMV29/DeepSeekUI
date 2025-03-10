using DeepSeekUI.Application.Dtos;
using DeepSeekUI.Application.Dtos.Identity;
using DeepSeekUI.Application.Interfaces;

namespace DeepSeekUI.Application.Handlers.UserHandlers
{
    public class GetUserHandler
    {
        private readonly IIdentityService _service;

        public GetUserHandler(IIdentityService service)
        {
            _service = service;
        }

        public async Task<Result<AppUserResponse>> HandleAsync(GetUserRequest request)
        {
            try
            {
                var user = await _service.GetUserAsync(request.Id);
                if (user is null)
                    return Result<AppUserResponse>.Failure("No existe un usuario con el Id proporcionado");

                return Result<AppUserResponse>.Success(user);
            }
            catch (Exception ex)
            {
                return Result<AppUserResponse>.Failure(ex.Message);
            }
        }
    }
}
