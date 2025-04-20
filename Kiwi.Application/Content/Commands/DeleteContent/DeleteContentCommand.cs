namespace Kiwi.Application.Content.Commands.DeleteContent;

public record DeleteContentCommand(int Id) : IRequest<bool>;