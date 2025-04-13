using FluentValidation;
using KiwiAPI.Models.Content;
using KiwiAPI.Services.ContentCreation;
using KiwiAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KiwiAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContentController(
    IContentService contentService, 
    IContentCreationFactory contentCreationFactory,
    IValidator<CreateContentRequest> createContentValidator,
    IValidator<UpdateContentRequest> updateContentValidator) 
    : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<string>>> Get()
    {
        var content = await contentService.GetContent();
        
        return Ok(content);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Content>> GetContent(int id)
    {
        var content = await contentService.GetContentById(id);

        return content is null ? NotFound() : Ok(content);
    }

    [HttpPost]
    public async Task<ActionResult<Content>> CreateContent(CreateContentRequest request)
    {
        var validationResults = await createContentValidator.ValidateAsync(request);

        if (!validationResults.IsValid)
        {
            return BadRequest(validationResults.Errors.Select(error => error.ErrorMessage));
        }

        var contentCreationService = contentCreationFactory.GetContentCreationService(request.ContentType);
        
        var content = await contentCreationService.CreateContent(request);

        return Ok(content);
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult<Content>> UpdateContent(int id, UpdateContentRequest request)
    {
        var validationResults = await updateContentValidator.ValidateAsync(request);

        if (!validationResults.IsValid)
        {
            return BadRequest(validationResults.Errors.Select(error => error.ErrorMessage));
        }
        
        var content = await contentService.UpdateContent(id, request);
        
        return Ok(content);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<bool>> DeleteContent(int id)
    {
        return await contentService.DeleteContent(id) ? NoContent() : BadRequest();
    }
}