using FluentValidation;
using Kiwi.WebApi.Models.Content;
using Kiwi.WebApi.Models.ToDo;
using Kiwi.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kiwi.WebApi.Controllers;

[ApiController]
[Route("/api")]
public class ToDoController(
    IToDoService toDoService, 
    IValidator<CreateToDoRequest> createToDoValidator,
    IValidator<UpdateToDoRequest> updateToDoValidator) 
    : ControllerBase
{
    [HttpGet("content/{id:int}/to-dos")]
    public async Task<ActionResult<ContentWithToDos>> GetContentWithToDos(int id)
    {
        var contentWithToDos = await toDoService.GetContentWithToDos(id);

        return contentWithToDos is null ? NotFound() : Ok(contentWithToDos);
    }

    [HttpPost("to-dos")]
    public async Task<ActionResult > CreateToDo(CreateToDoRequest request)
    {
        var validationResult = await createToDoValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
        }

        var result = await toDoService.CreateToDo(request);
        
        return result ? Created() : BadRequest();
    }

    [HttpPatch("to-dos/{id:int}")]
    public async Task<ActionResult> UpdateToDo(int id, UpdateToDoRequest request)
    {
        var validationResult = await updateToDoValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
        }

        var result = await toDoService.UpdateToDo(id, request);
        
        return result ? NoContent() : BadRequest();    
    }

    [HttpPatch("to-dos/{id:int}/toggle-completed")]
    public async Task<ActionResult> ToggleCompleted(int id)
    {
        var result = await toDoService.ToggleCompleted(id);
        
        return result ? NoContent() : BadRequest();
    }

    [HttpDelete("to-dos/{id:int}")]
    public async Task<ActionResult> DeleteToDo(int id)
    {
        var result = await toDoService.DeleteToDo(id);
        
        return result ? NoContent() : BadRequest();    
    }
}