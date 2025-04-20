using Kiwi.Application.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Kiwi.Application.Auth.Commands.LoginUser;

public class LoginUserCommandHandler(
    IUserRepository repository, 
    IPasswordHasher<Core.Entities.User> passwordHasher,
    ITokenService tokenService)
    : IRequestHandler<LoginUserCommand, LoginUserResult>
{
    public async Task<LoginUserResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await repository.GetByEmail(request.Email);
        if (user is null)
            return new LoginUserResult(false, null, "Invalid email or password");
        
        var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        if (result == PasswordVerificationResult.Failed)
            return new LoginUserResult(false, null, "Invalid email or password");
        
        var token = tokenService.GenerateToken(user);
        
        return new LoginUserResult(true, token, null);
    }
}