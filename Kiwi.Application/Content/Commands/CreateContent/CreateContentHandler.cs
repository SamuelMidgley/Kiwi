using Kiwi.Application.Interfaces;

namespace Kiwi.Application.Content.Commands.CreateContent;

public class CreateContentHandler(IContentRepository repository) : IRequestHandler<CreateContentCommand, int>
{
    public async Task<int> Handle(CreateContentCommand request, CancellationToken cancellationToken)
    {
        var content = new Core.Entities.Content
        {
            Title = request.Title,
            Description = request.Description,
            ContentType = request.ContentType,
            DateCreated = DateTime.UtcNow
        };

        return await repository.Create(content);
    }
}