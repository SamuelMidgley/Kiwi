namespace Kiwi.Application.Auth.Commands.LoginUser;

public record LoginUserResult(bool Success, string? Token, string? ErrorMessage);