using Kiwi.WebApi.Models.Content;
using Kiwi.WebApi.Repositories.Interfaces;
using Kiwi.WebApi.Services.Interfaces;

namespace Kiwi.WebApi.Services.ContentCreation;

public class ToDoContentCreationService(
    IContentRepository contentRepository,
    IContentService contentService) 
    : IContentCreationService
{
    public async Task<Content> CreateContent(CreateContentRequest request)
    {
        var createdContentId = await contentRepository.CreateContent(request);
        
        var content = await contentService.GetContentById(createdContentId);

        return content ?? throw new NullReferenceException("Content not found");
    }
}
