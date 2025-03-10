using DeepSeekUI.Application.Interfaces;
using DeepSeekUI.Application.Interfaces.Repositories;
using DeepSeekUI.Infrastructure.DeepSeekR1.Services;
using DeepSeekUI.Infrastructure.Persistence;
using DeepSeekUI.Infrastructure.Persistence.Repositories;
using DeepSeekUI.Infrastructure.Persistence.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DeepSeekUI.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DeepSeekContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DeepSeekConnectionString"));
            });
            services.AddHttpClient("DeepSeekClient", factory =>
            {
                factory.BaseAddress = new Uri("http://localhost:11434/api/chat/");
            });

            services.AddIdentity<IdentityUser<Guid>, IdentityRole<Guid>>(options=>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
            })
                .AddEntityFrameworkStores<DeepSeekContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<UserManager<IdentityUser<Guid>>>();
            services.AddScoped<SignInManager<IdentityUser<Guid>>>();

            services.AddScoped<IDeepSeekAPIService, DeepSeekAPIService>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            return services;
        }
    }
}
