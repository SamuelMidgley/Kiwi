using Kiwi.Application.Content.Commands;
using Kiwi.Core.Interfaces.Content;
using MediatR;

namespace Kiwi.Application.Content.Handlers;

public class CreateContentHandler(IContentRepository repository) : IRequestHandler<CreateContentCommand, int>
{
    public async Task<int> Handle(CreateContentCommand request, CancellationToken cancellationToken)
    {
        var content = new Core.Entities.Content.Content
        {
            Title = request.Title,
            Description = request.Description,
            ContentType = request.ContentType,
            DateCreated = DateTime.UtcNow
        };

        return await repository.CreateContent(content);
    }
}