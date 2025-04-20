using FluentAssertions;
using Kiwi.Application.Auth.Commands.LoginUser;
using Kiwi.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Kiwi.Application.UnitTests.Auth;

public class LoginUserHandlerTests
{
    private readonly Mock<IUserRepository> _mockRepo;
    private readonly Mock<IPasswordHasher<Core.Entities.User>> _mockHasher;
    private readonly Mock<ITokenService> _mockTokenService;
    private readonly LoginUserCommandHandler _handler;

    public LoginUserHandlerTests()
    {
        _mockRepo = new Mock<IUserRepository>();
        _mockHasher = new Mock<IPasswordHasher<Core.Entities.User>>();
        _mockTokenService = new Mock<ITokenService>();
        _handler = new LoginUserCommandHandler(
            _mockRepo.Object,
            _mockHasher.Object,
            _mockTokenService.Object);
    }
    
    [Fact]
    public async Task Handle_ShouldLoginUser_AndReturnToken()
    {
        // Arrange
        var command = new LoginUserCommand("Email", "Password");
        _mockRepo.Setup(r => r.GetByEmail(It.IsAny<string>()))
            .ReturnsAsync(new Core.Entities.User());
        
        _mockHasher.Setup(h 
                => h.VerifyHashedPassword(It.IsAny<Core.Entities.User>(), 
                    It.IsAny<string>(), 
                    It.IsAny<string>()))
            .Returns(PasswordVerificationResult.Success);

        _mockRepo.Setup(r => r.Create(It.IsAny<Core.Entities.User>()))
            .ReturnsAsync(1);
        
        _mockTokenService.Setup(ts => ts.GenerateToken(It.IsAny<Core.Entities.User>()))
            .Returns("token");
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        result.Success.Should().BeTrue();
        result.ErrorMessage.Should().BeNull();
        result.Token.Should().Be("token");
    }

    [Fact]
    public async Task Handle_ShouldReturnError_IfUserDoesNotExist()
    {
        // Arrange
        var command = new LoginUserCommand("Email", "Password");
        _mockRepo.Setup(r => r.GetByEmail(It.IsAny<string>()))
            .ReturnsAsync(null as Core.Entities.User);
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        result.Success.Should().BeFalse();
        result.ErrorMessage.Should().NotBeNull();
        result.Token.Should().BeNull();
    }

    [Fact]
    public async Task Handle_ShouldReturnError_IfPasswordDoesNotMatch()
    {
        // Arrange
        var command = new LoginUserCommand("Email", "Password");
        
        _mockRepo.Setup(r => r.GetByEmail(It.IsAny<string>()))
            .ReturnsAsync(null as Core.Entities.User);
        
        _mockHasher.Setup(h 
                => h.VerifyHashedPassword(It.IsAny<Core.Entities.User>(), 
                    It.IsAny<string>(), 
                    It.IsAny<string>()))
            .Returns(PasswordVerificationResult.Failed);
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        result.Success.Should().BeFalse();
        result.ErrorMessage.Should().NotBeNull();
        result.Token.Should().BeNull();
    }
}