using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Kiwi.Infrastructure.Data;

public class DataContext(IConfiguration configuration)
{
    private readonly string _connectionString = 
        configuration.GetConnectionString("DefaultConnection") 
        ?? throw new InvalidOperationException();

    public IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);
}