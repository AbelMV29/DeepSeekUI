using DeepSeekUI.Application.Handlers;
using DeepSeekUI.Application.Handlers.ChatHandlers;
using DeepSeekUI.Application.Handlers.MessageHandlers;
using DeepSeekUI.Application.Handlers.UserHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace DeepSeekUI.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<GetUserHandler>();

            services.AddScoped<SendMessageHandler>();
            services.AddScoped<GetFullChatHandler>();
            services.AddScoped<CreateChatHandler>();
            services.AddScoped<GetChatsUserHandler>();

            services.AddScoped<LoginUserHandler>();
            services.AddScoped<CreateUserHandler>();
            return services;
        }
    }
}
