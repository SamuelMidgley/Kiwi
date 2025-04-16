using Kiwi.WebApi.Models.Content;
using Kiwi.WebApi.Models.ToDo;

namespace Kiwi.WebApi.Services.Interfaces;

public interface IToDoService
{
    Task<ContentWithToDos?> GetContentWithToDos(int contentId);

    Task<bool> CreateToDo(CreateToDoRequest request);

    Task<bool> UpdateToDo(int id, UpdateToDoRequest request);

    Task<bool> DeleteToDo(int id);
    
    Task<bool> ToggleCompleted(int id);
}