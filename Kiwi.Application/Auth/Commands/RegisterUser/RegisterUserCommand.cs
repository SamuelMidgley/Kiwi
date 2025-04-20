namespace Kiwi.Application.Auth.Commands.RegisterUser;

public record RegisterUserCommand(string Name, string Email, string Password, string ConfirmPassword) 
    : IRequest<RegisterUserResult>;