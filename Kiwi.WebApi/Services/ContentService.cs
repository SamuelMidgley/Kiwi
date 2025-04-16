using Kiwi.WebApi.Models.Content;
using Kiwi.WebApi.Repositories.Interfaces;
using Kiwi.WebApi.Services.Interfaces;
using Kiwi.WebApi.Repositories;

namespace Kiwi.WebApi.Services;

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
        try
        {
            var createdContentId = await contentRepository.CreateContent(request);

            var content = await GetContentById(createdContentId);

            if (content is null)
            {
                logger.LogError("Content was created with ID {ContentId}, but could not be retrieved.", createdContentId);
                throw new KeyNotFoundException($"Created content with ID {createdContentId} not found.");
            }

            logger.LogInformation("Successfully created content with ID {ContentId}", createdContentId);
            return content;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while creating content.");
            throw;
        }
    }

    public async Task<Content> UpdateContent(int id, UpdateContentRequest request)
    {
        try
        {
            var isUpdated = await contentRepository.UpdateContent(id, request);

            if (!isUpdated)
            {
                logger.LogWarning("Content with ID {ContentId} was not found for update.", id);
                throw new KeyNotFoundException($"Content with ID {id} not found.");
            }

            var content = await GetContentById(id);

            if (content is null)
            {
                logger.LogError("Content with ID {ContentId} was updated but could not be retrieved.", id);
                throw new KeyNotFoundException($"Updated content with ID {id} not found.");
            }

            logger.LogInformation("Successfully updated content with ID {ContentId}", id);
            return content;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while updating content with ID {ContentId}", id);
            throw;
        }
    }

    public async Task<bool> DeleteContent(int id)
    {
        try
        {
            var result = await contentRepository.DeleteContent(id);

            if (!result)
            {
                logger.LogWarning("Failed to delete content with ID {ContentId} - not found.", id);
            }
            else
            {
                logger.LogInformation("Successfully deleted content with ID {ContentId}", id);
            }

            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while deleting content with ID {ContentId}", id);
            throw;
        }
    }
}