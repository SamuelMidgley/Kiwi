using Dapper;
using Kiwi.WebApi.Helpers;
using Kiwi.WebApi.Models.ToDo;
using Kiwi.WebApi.Repositories.Interfaces;

namespace Kiwi.WebApi.Repositories;

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