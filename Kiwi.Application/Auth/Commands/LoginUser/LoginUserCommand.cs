namespace Kiwi.Application.Auth.Commands.LoginUser;

public record LoginUserCommand(string Email, string Password) : IRequest<LoginUserResult>;