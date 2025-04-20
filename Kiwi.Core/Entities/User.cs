using Microsoft.AspNetCore.Identity;

namespace Kiwi.Core.Entities;

public class User
{
    public int Id { get; init; }
    
    public DateTime DateCreated { get; init; }
    
    public DateTime? DateUpdated { get; init; }
    
    public string Name { get; init; }
    
    public string Email { get; init; }
    
    public string PasswordHash { get; set; }
    
    public bool EmailConfirmed { get; init; }
    
    public void SetPassword(string password, IPasswordHasher<User> hasher)
    {
        PasswordHash = hasher.HashPassword(this, password);
    }
}