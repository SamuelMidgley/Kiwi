using Kiwi.Application.Interfaces;

namespace Kiwi.Application.Content.Queries.GetContentById;

using Content = Core.Entities.Content;

public class GetContentByIdQueryHandler(IContentRepository repository) :IRequestHandler<GetContentByIdQuery, Content>
{
    public async Task<Content> Handle(GetContentByIdQuery request, CancellationToken cancellationToken)
    {
        var content = await repository.GetById(request.Id);
        
        if (content is null) 
            throw new NotFoundException(request.Id.ToString(), "Content not found");
        
        return content;
    }
}