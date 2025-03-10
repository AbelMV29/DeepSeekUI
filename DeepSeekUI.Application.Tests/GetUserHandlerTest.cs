using DeepSeekUI.Application.Dtos;
using DeepSeekUI.Application.Dtos.Identity;
using DeepSeekUI.Application.Handlers.UserHandlers;
using DeepSeekUI.Application.Interfaces;
using FluentAssertions;
using Moq;

namespace DeepSeekUI.Application.Tests
{
    public class GetUserHandlerTest
    {
        private readonly Mock<IIdentityService> _serviceMock;
        private readonly GetUserHandler _handler;

        public GetUserHandlerTest()
        {
            _serviceMock = new Mock<IIdentityService>();
            _handler = new GetUserHandler(_serviceMock.Object);
        }
        [Fact]
        public async Task GetUser_WhenIdUserIsOkAnd_ShouldReturnResultSuccessFailed()
        {
            //Arrange
            GetUserRequest request = new GetUserRequest(Guid.NewGuid());

            AppUserResponse? expectedUser = new AppUserResponse(request.Id, "Usuario", "usuario@mail.com");
            _serviceMock
                .Setup(x => x.GetUserAsync(It.IsAny<Guid>()))
                .ReturnsAsync(expectedUser);

            //Act
            var result = await _handler.HandleAsync(request);

            //Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(expectedUser);
            _serviceMock.Verify(x => x.GetUserAsync(It.IsAny<Guid>()), Times.Once());
        }
        [Fact]
        public async Task GetUser_WhenUserDoesNotExist_ShouldReturnFailure()
        {
            //Arrange
            GetUserRequest request = new GetUserRequest(Guid.NewGuid());
            _serviceMock
                .Setup(x => x.GetUserAsync(It.IsAny<Guid>()))
                .ReturnsAsync((AppUserResponse?) null);

            //Act
            var result = await _handler.HandleAsync(request);

            //Assert
            result.Should().NotBeNull();
            result.Message.Should().Be("No existe un usuario con el Id proporcionado");
            result.IsSuccess.Should().BeFalse();
            result.Value.Should().BeNull();
            _serviceMock.Verify(x => x.GetUserAsync(It.IsAny<Guid>()), Times.Once());
        }
        [Fact]
        public async Task GetUser_WhenServicesThrowException_ShouldReturnFailure()
        {
            //Arrange
            GetUserRequest request = new GetUserRequest(Guid.NewGuid());
            _serviceMock
                .Setup(x => x.GetUserAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception("Unexpected Exception"));

            //Act
            var result = await _handler.HandleAsync(request);

            //Assert
            result.Should().NotBeNull();
            result.Message.Should().Be("Unexpected Exception");
            result.IsSuccess.Should().BeFalse();
            result.Value.Should().BeNull();
            _serviceMock.Verify(x => x.GetUserAsync(It.IsAny<Guid>()), Times.Once());
        }
    }
}
