using DeepSeekUI.Application.Dtos;
using DeepSeekUI.Application.Handlers.ChatHandlers;
using DeepSeekUI.Application.Handlers.MessageHandlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DeepSeekUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : Controller
    {
        private readonly SendMessageHandler _sendMessageHandler;
        private readonly CreateChatHandler _createChatHandler;
        public ChatController(SendMessageHandler sendMessageHandler, CreateChatHandler createChatHandler)
        {
            _sendMessageHandler = sendMessageHandler;
            _createChatHandler = createChatHandler;
        }

        [HttpPost("message")]
        public async Task<ActionResult<Result<DeepSeekShortResponse>>> SendMessage([FromBody] SendMessageRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (User.Identity.IsAuthenticated)
                request.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await _sendMessageHandler.HandleAsync(request);
            if (result.IsSuccess)
                return StatusCode(StatusCodes.Status200OK, result);

            return StatusCode(StatusCodes.Status400BadRequest, result.Message);
        }

        [Authorize]
        [HttpPost()]
        public async Task<ActionResult<Result<CreateChatResponse>>> CreateChat([FromBody] CreateChatRequest request)
        {
            request.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await _createChatHandler.HandleAsync(request);
            if (result.IsSuccess)
                return StatusCode(StatusCodes.Status200OK, result);

            return StatusCode(StatusCodes.Status400BadRequest, result.Message);
        }

        [HttpGet("{chatId}")]
        public IActionResult GetChat(Guid chatId)
        {
            return ViewComponent("Chat", chatId);
        }
    }
}
