using System.Data;
using Npgsql;

namespace Kiwi.WebApi.Helpers;

public class DataContext(IConfiguration configuration)
{
    private readonly string _connectionString = 
        configuration.GetConnectionString("DefaultConnection") 
        ?? throw new InvalidOperationException();

    public IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);
}