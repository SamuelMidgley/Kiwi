using Microsoft.AspNetCore.Mvc;

namespace KiwiAPI.Controllers;

[ApiController]
public class HelloWorldController : ControllerBase
{
    [HttpGet("api/hello-world")]
    public ActionResult<string> Get()
    {
        return Ok("Hello World!");
    }
}