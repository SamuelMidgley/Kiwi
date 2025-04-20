using Kiwi.Application.Common.Models;

namespace Kiwi.Application.Content.Queries.GetContent;

public record GetContentQuery(int PageSize, int PageNumber) 
    : IRequest<PaginatedList<Core.Entities.Content>>;