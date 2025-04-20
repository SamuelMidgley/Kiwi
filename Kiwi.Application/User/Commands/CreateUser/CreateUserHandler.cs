using Kiwi.Application.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Kiwi.Application.User.Commands.CreateUser;

public class CreateUserHandler(IUserRepository repository, IPasswordHasher<Core.Entities.User> passwordHasher) 
    : IRequestHandler<CreateUserCommand, int>
{
    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new Core.Entities.User
        {
            Name = request.Name,
            Email = request.Email,
        };
        
       user.SetPassword(request.Password, passwordHasher);
        
        return await repository.Create(user);
    }
}