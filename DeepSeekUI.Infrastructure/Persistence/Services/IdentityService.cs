using DeepSeekUI.Application.Dtos;
using DeepSeekUI.Application.Dtos.Identity;
using DeepSeekUI.Application.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DeepSeekUI.Infrastructure.Persistence.Services
{
    internal class IdentityService : IIdentityService
    {
        private readonly UserManager<IdentityUser<Guid>> _userManager;
        private readonly SignInManager<IdentityUser<Guid>> _signInManager;

        public IdentityService(UserManager<IdentityUser<Guid>> userManager, SignInManager<IdentityUser<Guid>> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<AppUserResponse?> GetUserAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            return null;
        }

        public async Task<Result<object>> LoginAsync(LoginUserRequest request)
        {
            try{
                IdentityUser<Guid>? user = await _userManager.FindByEmailAsync(request.Email);
                if(user is null)
                    return Result<object>.Failure("Usuario no encontrado");
                bool isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
                if(!isPasswordValid)
                    return Result<object>.Failure("Contraseña incorrecta");

                SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);
                if(!signInResult.Succeeded)
                    return Result<object>.Failure("Error al iniciar sesión");
                return Result<object>.Success(null);
            }
            catch(Exception ex)
            {
                return Result<object>.Failure(ex.Message);
            }
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<Result<object>> RegisterAsync(CreateUserRequest request)
        {
            try{
                IdentityUser<Guid>? user = await _userManager.FindByEmailAsync(request.Email) ?? await _userManager.FindByNameAsync(request.UserName);
                if(user is not null)
                    return Result<object>.Failure("El email y/o nombre de usuario ya pertenecen a una cueta");
                IdentityUser<Guid> newUser = new IdentityUser<Guid>(request.UserName)
                {
                    Email = request.Email,
                    NormalizedEmail = request.Email.ToUpper(),
                    NormalizedUserName = request.UserName.ToUpper(),
                    Id = Guid.NewGuid(),
                };

                IdentityResult createResult = await _userManager.CreateAsync(newUser, request.Password);
                if(!createResult.Succeeded)
                    return Result<object>.Failure(string.Join(", ", createResult.Errors.Select(e=>e.Description)));
                return Result<object>.Success(null);
            }
            catch(Exception ex)
            {
                return Result<object>.Failure(ex.Message);
            }
        }
    }
}
