namespace Kiwi.Application.User.Commands.UpdateUser;

public record UpdateUserCommand(int Id, string? Name, string? Email) : IRequest<bool>;