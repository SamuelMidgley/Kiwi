using Kiwi.Application.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Kiwi.Application.Auth.Commands.RegisterUser;

public class RegisterUserCommandHandler(
    IUserRepository repository, 
    IPasswordHasher<Core.Entities.User> passwordHasher,
    ITokenService tokenService) 
    : IRequestHandler<RegisterUserCommand, RegisterUserResult>
{
    public async Task<RegisterUserResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await repository.GetByEmail(request.Email);
        if (existingUser is not null)
            return new RegisterUserResult(false, null, "User with that email already exists.");
        
        var user = new Core.Entities.User
        {
            Name = request.Name,
            Email = request.Email,
        };
        
        user.SetPassword(request.Password, passwordHasher);
        
        var userId = await repository.Create(user);
        if (userId == 0)
            return new RegisterUserResult(false, null, "Error occurred creating user");
        
        var token = tokenService.GenerateToken(user);
        
        return new RegisterUserResult(true, token, null);
    }
}