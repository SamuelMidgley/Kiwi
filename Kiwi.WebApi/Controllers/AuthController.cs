using Kiwi.Application.Auth.Commands.LoginUser;
using Kiwi.Application.Auth.Commands.RegisterUser;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Kiwi.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(ISender sender) : ControllerBase
{
    [HttpPost("login")]
    public async Task<Results<Ok<LoginUserResult>, BadRequest<LoginUserResult>>> Login(LoginUserCommand request)
    {
        var result = await sender.Send(request);

        if (result.Success == false)
            return TypedResults.BadRequest(result);

        return TypedResults.Ok(result);
    }

    [HttpPost("register")]
    public async Task<Results<Ok<RegisterUserResult>, BadRequest<RegisterUserResult>>> Register(RegisterUserCommand request)
    {
        var result = await sender.Send(request);
        
        if (result.Success == false)
            return TypedResults.BadRequest(result);

        return TypedResults.Ok(result);
    }
}