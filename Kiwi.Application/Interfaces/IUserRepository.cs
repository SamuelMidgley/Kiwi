namespace Kiwi.Application.Interfaces;

using User = Core.Entities.User;

public interface IUserRepository
{
    Task<User?> GetById(int id);
    
    Task<User?> GetByEmail(string email);
    
    Task<int> Create(User user);
    
    Task<int> Update(int id, string? name, string? email);
    
    Task<int> Delete(int id);
}