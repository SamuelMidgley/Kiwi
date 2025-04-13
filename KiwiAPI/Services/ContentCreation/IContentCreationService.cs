using KiwiAPI.Models.Content;

namespace KiwiAPI.Services.ContentCreation;

public interface IContentCreationService
{
    Task<Content> CreateContent(CreateContentRequest request);
}