namespace Kiwi.Application.User.Commands.DeleteUser;

public record DeleteUserCommand(int Id) : IRequest<bool>;