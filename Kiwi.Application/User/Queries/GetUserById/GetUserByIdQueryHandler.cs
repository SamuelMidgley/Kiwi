using Kiwi.Application.Interfaces;

namespace Kiwi.Application.User.Queries.GetUserById;

public class GetUserByIdQueryHandler(IUserRepository repository) 
    : IRequestHandler<GetUserByIdQuery, Core.Entities.User?>
{
    public async Task<Core.Entities.User?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await repository.GetById(request.Id);
        
        if (user is null)
            throw new NotFoundException(request.Id.ToString(), "User");
        
        return user;
    }
}