using DeepSeekUI.Application.Dtos;
using DeepSeekUI.Application.Handlers.ChatHandlers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DeepSeekUI.Web.ViewComponents
{
    [ViewComponent]
    public class SideChatViewComponent : ViewComponent
    {
        private readonly GetChatsUserHandler _handler;
        public SideChatViewComponent(GetChatsUserHandler handler)
        {
            _handler = handler;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User.Identity is not null && User.Identity.IsAuthenticated)
            {
                Guid actualUserId = new Guid(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                Result<GetChatsUserResponse[]> chatsForUser = await _handler.HandleAsync(new GetChatsUserRequest(actualUserId));

                if (!chatsForUser.IsSuccess)
                {
                    ViewBag.SideChatError = chatsForUser.Message;
                    return View((GetChatsUserResponse[])[]);
                }
                return View(chatsForUser.Value);
            }
            return View((GetChatsUserResponse[])[]);
        }
    }
}
