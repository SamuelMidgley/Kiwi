using Kiwi.Application.Common.Models;
using Kiwi.Application.Interfaces;

namespace Kiwi.Application.Content.Queries.GetContent;

using Content = Core.Entities.Content;

public class GetContentQueryHandler(IContentRepository repository)
    : IRequestHandler<GetContentQuery, PaginatedList<Content>>
{
    public Task<PaginatedList<Content>> Handle(GetContentQuery request, CancellationToken cancellationToken)
    {
        return repository.Get(request.PageNumber, request.PageSize);
    }
}