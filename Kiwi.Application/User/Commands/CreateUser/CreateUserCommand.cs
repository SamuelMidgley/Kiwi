namespace Kiwi.Application.User.Commands.CreateUser;

public record CreateUserCommand(string Name, string Email, string Password, string ConfirmPassword) : IRequest<int>;