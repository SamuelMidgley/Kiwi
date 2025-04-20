namespace Kiwi.Application.User.Queries.GetUserById;

public record GetUserByIdQuery(int Id) : IRequest<Core.Entities.User?>;