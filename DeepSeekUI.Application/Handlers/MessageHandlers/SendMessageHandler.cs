using DeepSeekUI.Application.Dtos;
using DeepSeekUI.Application.Interfaces;
using DeepSeekUI.Application.Interfaces.Repositories;
using DeepSeekUI.Domain.Entities;

namespace DeepSeekUI.Application.Handlers.MessageHandlers
{
    public class SendMessageHandler
    {
        private readonly IDeepSeekAPIService _service;
        private readonly IChatRepository _chatRepository;
        private readonly IMessageRepository _messageRepository;

        public SendMessageHandler(IDeepSeekAPIService service, IChatRepository chatRepository, IMessageRepository messageRepository)
        {
            _service = service;
            _chatRepository = chatRepository;
            _messageRepository = messageRepository;
        }

        public async Task<Result<DeepSeekShortResponse>> HandleAsync(SendMessageRequest request)
        {
            using var cts = new CancellationTokenSource();
            var modelResponseTask = _service.SendMessage(request.Message, cts.Token);

            if(request.UserId is not null && request.ChatId is not null)
            {
                try
                {
                    var chatValid = await _chatRepository.GetChatValidForUser(request.ChatId.Value, request.UserId.Value);

                    if (chatValid is null)
                    {
                        cts.Cancel();
                        return Result<DeepSeekShortResponse>.Failure("Chat invalido");
                    }

                    var messageQuestion = chatValid.CreateMessageForChat(request.Message, MessageType.Question);
                    var modelResponse = await modelResponseTask;
                    if (!modelResponse.IsSuccess)
                        return modelResponse;
                    var messageResponse = chatValid.CreateMessageForChat(modelResponse.Value!.Message, MessageType.Response);
                    chatValid.SetLastModified();
                    await _messageRepository.AddArrangeAsync(messageQuestion, messageResponse);
                    await _messageRepository.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return Result<DeepSeekShortResponse>.Failure(ex.Message);
                }
            }

            var modelResponseWithoutIf = modelResponseTask.IsCompleted ? modelResponseTask.Result : await modelResponseTask;

            return modelResponseWithoutIf;
        }
    }
}
