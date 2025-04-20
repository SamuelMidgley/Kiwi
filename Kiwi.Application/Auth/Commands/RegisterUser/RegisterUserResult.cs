namespace Kiwi.Application.Auth.Commands.RegisterUser;

public record RegisterUserResult(bool Success, string? Token, string? ErrorMessage);