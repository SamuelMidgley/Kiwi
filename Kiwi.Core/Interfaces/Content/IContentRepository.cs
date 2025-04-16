namespace Kiwi.Core.Interfaces.Content;

public interface IContentRepository
{
    // Task<IEnumerable<Entities.Content.Content>> GetContent();
    //
    // Task<Entities.Content.Content?> GetContentById(int id);
    
    Task<int> CreateContent(Entities.Content.Content request);
    
    // Task<bool> UpdateContent(int id, Entities.Content.Content request);
    //
    // Task<bool> DeleteContent(int id);
}