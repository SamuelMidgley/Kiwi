using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Kiwi.WebApi.Controllers;

[ApiController]
public class HelloWorldController : ControllerBase
{
    [HttpGet("api/hello-world")]
    public Ok<string> Get()
    {
        return TypedResults.Ok("Hello World");
    }
}