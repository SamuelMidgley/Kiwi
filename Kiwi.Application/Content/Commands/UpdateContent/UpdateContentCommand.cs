namespace Kiwi.Application.Content.Commands.UpdateContent;

public record UpdateContentCommand(int Id, string? Title, string? Description) : IRequest<bool>;