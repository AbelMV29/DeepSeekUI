using DeepSeekUI.Application.Dtos;
using DeepSeekUI.Application.Handlers.MessageHandlers;
using DeepSeekUI.Application.Interfaces;
using DeepSeekUI.Application.Interfaces.Repositories;
using DeepSeekUI.Domain.Entities;
using FluentAssertions;
using Moq;

namespace DeepSeekUI.Application.Tests
{
    public class SendMessageHandlerTest
    {
        private readonly Mock<IDeepSeekAPIService> _serviceMock;
        private readonly Mock<IChatRepository> _chatRepositoryMock;
        private readonly Mock<IMessageRepository> _messageRepositoryMock;
        private readonly SendMessageHandler _handler;
        public SendMessageHandlerTest()
        {
            _serviceMock = new Mock<IDeepSeekAPIService>();
            _chatRepositoryMock = new Mock<IChatRepository>();
            _messageRepositoryMock = new Mock<IMessageRepository>();
            _handler = new SendMessageHandler(_serviceMock.Object, _chatRepositoryMock.Object, _messageRepositoryMock.Object);
        }
        [Fact]
        public async Task SendMessage_WhenChatIdIsInvalidForSpecificUser_ShouldReturnResultFailure()
        {
            //Arrange
            var request = CreateSendMessageRequest(Guid.NewGuid(), Guid.NewGuid());
            _serviceMock
                .Setup(x => x.SendMessage(request.Message, It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(Result<DeepSeekShortResponse>.Success(new DeepSeekShortResponse { })));
            _chatRepositoryMock
                .Setup(x => x.GetChatValidForUser(request.ChatId.Value, request.UserId.Value))
                .ReturnsAsync((Chat?) null);

            //Act
            var result = await _handler.HandleAsync(request);

            //Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Be("Chat invalido");
            _serviceMock.Verify(x=>x.SendMessage(request.Message, CancellationToken.None), Times.AtMostOnce());
            _chatRepositoryMock.Verify(x => x.GetChatValidForUser(request.ChatId.Value, request.UserId.Value), Times.Once());
        }

        [Fact]
        public async Task SendMessage_WhenThrowUnexpectedException_ShouldReturnResultFailure()
        {
            //Arrange
            var request = CreateSendMessageRequest(Guid.NewGuid(), Guid.NewGuid());
            _serviceMock
                .Setup(x => x.SendMessage(request.Message, It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(Result<DeepSeekShortResponse>.Success(new DeepSeekShortResponse { })));
            _chatRepositoryMock
                .Setup(x => x.GetChatValidForUser(request.ChatId.Value, request.UserId.Value))
                .ThrowsAsync(new Exception("Unexected Exception"));

            //Act
            var result = await _handler.HandleAsync(request);

            //Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Be("Unexected Exception");
            _serviceMock.Verify(x => x.SendMessage(request.Message, CancellationToken.None), Times.AtMostOnce());
            _chatRepositoryMock.Verify(x => x.GetChatValidForUser(request.ChatId.Value, request.UserId.Value), Times.Once());
        }

        [Fact]
        public async Task SendMessage_WhenDeepSeekServiceFail_ShouldReturnResultFailure()
        {
            //Arrange
            var request = CreateSendMessageRequest(Guid.NewGuid(), Guid.NewGuid());
            _serviceMock
                .Setup(x => x.SendMessage(request.Message, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<DeepSeekShortResponse>.Failure("Error al llamar a la API local de DeepSeek"));
            _chatRepositoryMock
                .Setup(x => x.GetChatValidForUser(request.ChatId.Value, request.UserId.Value))
                .ReturnsAsync(new Chat("my chat", request.UserId.Value, new List<Message>()));

            //Act
            var result = await _handler.HandleAsync(request);

            //Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Be("Error al llamar a la API local de DeepSeek");
            _serviceMock.Verify(x => x.SendMessage(request.Message, CancellationToken.None), Times.AtMostOnce());
            _chatRepositoryMock.Verify(x => x.GetChatValidForUser(request.ChatId.Value, request.UserId.Value), Times.Once());
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("00000000-0000-0000-0000-000000000000", "11111111-1111-1111-1111-111111111111")]
        public async Task SendMessage_WhenAllIsOkWithUser_ShouldReturnResultSuccess(string? chatId, string? userId)
        {
            //Arrange
            var request = CreateSendMessageRequest(chatId, userId);

            _serviceMock
                .Setup(x => x.SendMessage(request.Message, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result<DeepSeekShortResponse>.Success(new DeepSeekShortResponse("If you want improve your project then you should...", DateTime.UtcNow)));
            if(request.UserId is not null && request.ChatId is not null)
            {
                _chatRepositoryMock
                    .Setup(x => x.GetChatValidForUser(It.IsAny<Guid>(), It.IsAny<Guid>()))
                    .ReturnsAsync(new Chat("my chat", request.UserId.Value, new List<Message>()));
                _messageRepositoryMock
                    .Setup(x => x.AddArrangeAsync(It.IsAny<Message>(), It.IsAny<Message>()))
                    .Callback(() => Console.WriteLine("Add arrange called"))
                    .Returns(Task.CompletedTask);
                _messageRepositoryMock
                    .Setup(x => x.SaveChangesAsync())
                    .Returns(Task.CompletedTask);
            }
            

            //Act
            var result = await _handler.HandleAsync(request);

            //Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Message.Should().Be("If you want improve your project then you should...");
            _serviceMock.Verify(x => x.SendMessage(request.Message, CancellationToken.None), Times.AtMostOnce());
            if (request.UserId is not null && request.ChatId is not null)
            {
                _chatRepositoryMock.Verify(x => x.GetChatValidForUser(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once());
                _messageRepositoryMock.Verify(x => x.AddArrangeAsync(It.IsAny<Message>(), It.IsAny<Message>()), Times.Once());
                _messageRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once());
            }
        }

        private SendMessageRequest CreateSendMessageRequest(Guid? chatId, Guid? userId)
        {
            return new SendMessageRequest()
            {
                ChatId = chatId,
                Message = "How to improve my personal project",
                UserId = userId
            };
        }

        private SendMessageRequest CreateSendMessageRequest(string? chatId, string? userId)
        {
            return new SendMessageRequest()
            {
                ChatId = chatId is null ? null : new Guid(chatId),
                Message = "How to improve my personal project",
                UserId = userId is null ? null : new Guid(userId)
            };
        }
    }
}