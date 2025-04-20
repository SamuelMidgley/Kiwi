namespace Kiwi.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(Core.Entities.User user);
}