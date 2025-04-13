using KiwiAPI.Models.Content;
using KiwiAPI.Repositories;
using KiwiAPI.Services.Interfaces;
using KeyNotFoundException = System.Collections.Generic.KeyNotFoundException;

namespace KiwiAPI.Services;

public class ContentService(
    IContentRepository contentRepository, 
    ILogger<ContentService> logger) 
    : IContentService
{
    public async Task<IEnumerable<Content>> GetContent()
    {
        try
        {
            return await contentRepository.GetContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while retrieving all content.");
            throw;
        }
    }

    public async Task<Content?> GetContentById(int id)
    {
        if (id <= 0)
        {
            logger.LogWarning("Invalid content ID provided: {ContentId}", id);
            return null;
        }

        try
        {
            return await contentRepository.GetContentById(id);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while retrieving content with ID {ContentId}", id);
            throw;
        }    
    }

    public async Task<Content> CreateContent(CreateContentRequest request)
    {
        var createdContentId = await contentRepository.CreateContent(request);
        
        var content = await GetContentById(createdContentId);

        if (content is null)
        {
            throw new KeyNotFoundException();
        }
        
        return content;
    }

    public async Task<Content> UpdateContent(int id, UpdateContentRequest request)
    {
        var isUpdated = await contentRepository.UpdateContent(id, request);

        if (!isUpdated)
        {
            throw new KeyNotFoundException();
        }
        
        var content = await GetContentById(id);

        if (content is null)
        {
            throw new KeyNotFoundException();
        }
        
        return content;
    }

    public Task<bool> DeleteContent(int id)
    {
        return contentRepository.DeleteContent(id);
    }
}