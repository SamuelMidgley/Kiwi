using FluentValidation;
using Kiwi.WebApi.Models.Content;
using Kiwi.WebApi.Services.ContentCreation;
using Kiwi.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kiwi.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContentController(
    IContentService contentService, 
    IContentCreationFactory contentCreationFactory,
    IValidator<CreateContentRequest> createContentValidator,
    IValidator<UpdateContentRequest> updateContentValidator,
    ILogger<ContentController> logger) 
    : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<string>>> Get()
    {
        try
        {
            var content = await contentService.GetContent();
            return Ok(content);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to get content.");
            return StatusCode(StatusCodes.Status500InternalServerError, 
                "An error occurred while retrieving content.");
        }
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Content>> GetContentById(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid content ID.");
        }

        try
        {
            var content = await contentService.GetContentById(id);
            return content is null ? NotFound() : Ok(content);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to get content with ID {ContentId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError,
                "An error occurred while retrieving content.");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Content>> CreateContent(CreateContentRequest request)
    {
        var validationResults = await createContentValidator.ValidateAsync(request);

        if (!validationResults.IsValid)
        {
            return BadRequest(validationResults.Errors.Select(e => e.ErrorMessage));
        }

        try
        {
            var contentCreationService = contentCreationFactory.GetContentCreationService(request.ContentType);
            var content = await contentCreationService.CreateContent(request);

            return CreatedAtAction(nameof(GetContentById), new { id = content.Id }, content);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to create content.");
            return StatusCode(StatusCodes.Status500InternalServerError,
                "An error occurred while creating content.");
        }
    }

    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Content>> UpdateContent(int id, UpdateContentRequest request)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid content ID.");
        }

        var validationResults = await updateContentValidator.ValidateAsync(request);

        if (!validationResults.IsValid)
        {
            return BadRequest(validationResults.Errors.Select(e => e.ErrorMessage));
        }

        try
        {
            var content = await contentService.UpdateContent(id, request);
            return Ok(content);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to update content with ID {ContentId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, 
                "An error occurred while updating content.");
        }
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<bool>> DeleteContent(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Invalid content ID.");
        }

        try
        {
            var result = await contentService.DeleteContent(id);
            return result ? NoContent() : NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to delete content with ID {ContentId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError,
                "An error occurred while deleting content.");
        }
    }
}