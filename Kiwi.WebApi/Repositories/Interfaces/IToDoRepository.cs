using Kiwi.WebApi.Models.ToDo;

namespace Kiwi.WebApi.Repositories.Interfaces;

public interface IToDoRepository
{
    Task<IEnumerable<ToDo>> GetContentToDos(int contentId);

    Task<bool> CreateToDo(CreateToDoRequest request);

    Task<bool> UpdateToDo(int id, UpdateToDoRequest request);

    Task<bool> DeleteToDo(int id);
    
    Task<bool> ToggleCompleted(int id);
}
