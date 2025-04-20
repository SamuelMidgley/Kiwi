using Kiwi.Application.Common.Models;

namespace Kiwi.Application.Interfaces;

using Content = Core.Entities.Content;

public interface IContentRepository
{
    Task<PaginatedList<Content>> Get(int pageNumber, int pageSize);
    
    Task<Content?> GetById(int id);
    
    Task<int> Create(Content request);
    
    Task<int> Update(int id, string? title, string? description);
    
    Task<int> Delete(int id);
}