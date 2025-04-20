using Kiwi.Application.Interfaces;

namespace Kiwi.Application.User.Commands.DeleteUser;

public class DeleteUserHandler(IUserRepository repository) : IRequestHandler<DeleteUserCommand, bool>
{
    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var rowsAffected = await repository.Delete(request.Id);

        if (rowsAffected == 0)
            throw new NotFoundException(request.Id.ToString(), "User");
        
        return true;
    }
}