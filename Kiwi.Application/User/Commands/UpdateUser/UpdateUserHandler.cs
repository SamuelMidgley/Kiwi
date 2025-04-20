using Kiwi.Application.Interfaces;

namespace Kiwi.Application.User.Commands.UpdateUser;

public class UpdateUserHandler(IUserRepository repository) : IRequestHandler<UpdateUserCommand, bool>
{
    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var rowsAffected = await repository.Update(request.Id ,request.Name, request.Email);

        if (rowsAffected == 0)
            throw new NotFoundException(request.Id.ToString(), "User");
        
        return true;
    }
}