using Kiwi.WebApi.Models.Content;

namespace Kiwi.WebApi.Services.Interfaces;

public interface IContentService
{
    Task<IEnumerable<Content>> GetContent();
    
    Task<Content?> GetContentById(int id);
    
    Task<Content> UpdateContent(int id, UpdateContentRequest request);
    
    Task<bool> DeleteContent(int id);
}