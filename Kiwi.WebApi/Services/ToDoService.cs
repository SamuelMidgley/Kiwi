using Kiwi.WebApi.Models.Content;
using Kiwi.WebApi.Models.ToDo;
using Kiwi.WebApi.Repositories.Interfaces;
using Kiwi.WebApi.Services.Interfaces;

namespace Kiwi.WebApi.Services;

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