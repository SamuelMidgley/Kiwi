using FluentAssertions;
using Kiwi.Application.Auth.Commands.RegisterUser;
using Kiwi.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Kiwi.Application.UnitTests.Auth;

public class RegisterUserHandlerTests
{
    private readonly Mock<IUserRepository> _mockRepo;
    private readonly Mock<IPasswordHasher<Core.Entities.User>> _mockHasher;
    private readonly Mock<ITokenService> _mockTokenService;
    private readonly RegisterUserCommandHandler _handler;

    public RegisterUserHandlerTests()
    {
        _mockRepo = new Mock<IUserRepository>();
        _mockHasher = new Mock<IPasswordHasher<Core.Entities.User>>();
        _mockTokenService = new Mock<ITokenService>();
        _handler = new RegisterUserCommandHandler(
            _mockRepo.Object,
            _mockHasher.Object,
            _mockTokenService.Object);
    }
    
    [Fact]
    public async Task Handle_ShouldCreateUser_AndReturnToken()
    {
        // Arrange
        var command = new RegisterUserCommand("Name", "Email", "Password", "Password");
        _mockRepo.Setup(r => r.GetByEmail(It.IsAny<string>()))
            .Returns(Task.FromResult<Core.Entities.User?>(null));
        
        _mockHasher.Setup(h 
                => h.HashPassword(It.IsAny<Core.Entities.User>(), It.IsAny<string>()))
            .Returns("hashedPassword");

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
    public async Task Handle_ShouldRejectDuplicateEmail()
    {
        // Arrange
        var command = new RegisterUserCommand("Name", "Email", "Password", "Password");
        _mockRepo.Setup(r => r.GetByEmail(It.IsAny<string>()))
            .ReturnsAsync(new Core.Entities.User());
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        result.Success.Should().BeFalse();
        result.ErrorMessage.Should().NotBeNull();
        result.Token.Should().BeNull();
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenErrorOccursCreatingUser()
    {
        // Arrange
        var command = new RegisterUserCommand("Name", "Email", "Password", "Password");
        
        _mockRepo.Setup(r => r.GetByEmail(It.IsAny<string>()))
            .Returns(Task.FromResult<Core.Entities.User?>(null));
        
        _mockHasher.Setup(h 
                => h.HashPassword(It.IsAny<Core.Entities.User>(), It.IsAny<string>()))
            .Returns("hashedPassword");

        _mockRepo.Setup(r => r.Create(It.IsAny<Core.Entities.User>()))
            .ReturnsAsync(0);
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        result.Success.Should().BeFalse();
        result.ErrorMessage.Should().NotBeNull();
        result.Token.Should().BeNull();
    }
}