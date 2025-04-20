using Kiwi.Application.Common.Models;
using Kiwi.Application.Content.Commands.CreateContent;
using Kiwi.Application.Content.Commands.DeleteContent;
using Kiwi.Application.Content.Commands.UpdateContent;
using Kiwi.Application.Content.Queries.GetContent;
using Kiwi.Application.Content.Queries.GetContentById;
using Kiwi.Core.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Kiwi.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContentController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<Ok<PaginatedList<Content>>> Get([FromQuery] GetContentQuery query)
    {
        var content = await sender.Send(query);
        
        return TypedResults.Ok(content);
    }
    
    [HttpGet("{id:int}")]
    public async Task<Ok<Content>> Get(int id)
    {
        var content = await sender.Send(new GetContentByIdQuery(id));
        
        return TypedResults.Ok(content);
    }

    [HttpPost]
    public async Task<Created<int>> Create([FromBody] CreateContentCommand command)
    {
        var id = await sender.Send(command);
        
        return TypedResults.Created($"/content/{id}", id);
    }

    [HttpPatch("{id:int}")]
    public async Task<Results<NoContent, BadRequest>> Update([FromRoute] int id, [FromBody] UpdateContentCommand command)
    {
        if (id != command.Id)
            return TypedResults.BadRequest();
        
        await sender.Send(command);
        
        return TypedResults.NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<NoContent> Delete(int id)
    {
        await sender.Send(new DeleteContentCommand(id));

        return TypedResults.NoContent();
    }
}