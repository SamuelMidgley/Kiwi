using Dapper;
using KiwiAPI.Helpers;
using KiwiAPI.Models.ToDo;

namespace KiwiAPI.Repositories;

public interface IToDoRepository
{
    Task<IEnumerable<ToDo>> GetContentToDos(int contentId);

    Task<bool> CreateToDo(CreateToDoRequest request);

    Task<bool> UpdateToDo(int id, UpdateToDoRequest request);

    Task<bool> DeleteToDo(int id);
    
    Task<bool> ToggleCompleted(int id);
}

public class ToDoRepository(DataContext context) : IToDoRepository
{
    public async Task<IEnumerable<ToDo>> GetContentToDos(int contentId)
    {
        using var connection = context.CreateConnection();
        
        var results = await connection.QueryAsync<ToDo>("""
            SELECT *
            FROM to_dos
            WHERE to_dos.content_id = @contentId
        """, new { contentId });
        
        return results;
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