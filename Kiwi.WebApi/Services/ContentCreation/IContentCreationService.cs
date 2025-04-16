using Kiwi.WebApi.Models.Content;

namespace Kiwi.WebApi.Services.ContentCreation;

public interface IContentCreationService
{
    Task<Content> CreateContent(CreateContentRequest request);
}