using Kiwi.Core.Entities.Content;
using MediatR;

namespace Kiwi.Application.Content.Commands;

public record CreateContentCommand(string Title, string? Description, ContentType ContentType) : IRequest<int>;