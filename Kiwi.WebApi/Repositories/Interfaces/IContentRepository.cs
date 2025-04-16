using Kiwi.WebApi.Models.Content;

namespace Kiwi.WebApi.Repositories.Interfaces;

public interface IContentRepository
{
    Task<IEnumerable<Content>> GetContent();
    
    Task<Content?> GetContentById(int id);
    
    Task<int> CreateContent(CreateContentRequest request);
    
    Task<bool> UpdateContent(int id, UpdateContentRequest request);
    
    Task<bool> DeleteContent(int id);
}