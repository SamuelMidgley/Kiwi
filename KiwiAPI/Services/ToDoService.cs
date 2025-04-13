using KiwiAPI.Models.Content;
using KiwiAPI.Models.ToDo;
using KiwiAPI.Repositories;

namespace KiwiAPI.Services;

public interface IToDoService
{
    Task<ContentWithToDos?> GetContentWithToDos(int contentId);

    Task<bool> CreateToDo(CreateToDoRequest request);

    Task<bool> UpdateToDo(int id, UpdateToDoRequest request);

    Task<bool> DeleteToDo(int id);
    
    Task<bool> ToggleCompleted(int id);
}

public class ToDoService(IContentRepository contentRepository, IToDoRepository toDoRepository) : IToDoService
{
    public async Task<ContentWithToDos?> GetContentWithToDos(int contentId)
    {
        var content = await contentRepository.GetContentById(contentId) as ContentWithToDos;

        if (content is null)
        {
            throw new KeyNotFoundException();
        }

        var toDos = await toDoRepository.GetContentToDos(contentId);

        content.ToDos = toDos;
        
        return content;
    }

    public Task<bool> CreateToDo(CreateToDoRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateToDo(int id, UpdateToDoRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteToDo(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ToggleCompleted(int id)
    {
        throw new NotImplementedException();
    }
}