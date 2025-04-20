namespace Kiwi.Application.Content.Queries.GetContentById;

public record GetContentByIdQuery(int Id) 
    : IRequest<Core.Entities.Content>;