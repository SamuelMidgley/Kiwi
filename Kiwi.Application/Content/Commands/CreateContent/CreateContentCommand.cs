using Kiwi.Core.Entities;

namespace Kiwi.Application.Content.Commands.CreateContent;

public record CreateContentCommand(string Title, string? Description, ContentType ContentType) : IRequest<int>;