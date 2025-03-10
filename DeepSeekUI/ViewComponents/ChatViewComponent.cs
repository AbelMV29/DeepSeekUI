using DeepSeekUI.Application.Dtos;
using DeepSeekUI.Application.Handlers.ChatHandlers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DeepSeekUI.Web.ViewComponents
{
    [ViewComponent]
    public class ChatViewComponent : ViewComponent
    {
        private readonly GetFullChatHandler _handler;
        public ChatViewComponent(GetFullChatHandler handler)
        {
            _handler = handler;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid? chatId)
        {
            if(!User.Identity!.IsAuthenticated || chatId is null)
            {
                return View((GetFullChatResponse?)null);
            }

            Guid actualUserId = new Guid(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            Result<GetFullChatResponse> result = await _handler.HandleAsync(new GetFullChatRequest(chatId.Value, actualUserId));

            if(!result.IsSuccess)
            {
                ViewBag.ChatError = result.Message;
                return View((GetFullChatResponse?)null);
            }
            return View(result.Value);
        }
    }
}
