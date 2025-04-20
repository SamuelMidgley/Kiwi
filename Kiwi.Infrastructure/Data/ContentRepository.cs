using Kiwi.Application.Common.Models;
using Kiwi.Application.Interfaces;
using Kiwi.Core.Entities;
using Kiwi.Infrastructure.Common;

namespace Kiwi.Infrastructure.Data;

public class ContentRepository(DataContext context) : IContentRepository
{
    public async Task<PaginatedList<Content>> Get(int pageNumber, int pageSize)
    {
        const string sql = """
                               SELECT * 
                               FROM Content
                           """;
        using var connection = context.CreateConnection();

        return await connection.ToPaginatedListAsync<Content>(
            sql,
            parameters: null,
            pageNumber,
            pageSize);
    }
    
    public async Task<Content?> GetById(int id)
    {
        const string sql = """
                               SELECT * 
                               FROM Content
                               WHERE id = @id
                           """;
        using var connection = context.CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<Content>(
            sql, 
            new { id });
    }

    public async Task<int> Create(Content request)
    {
        var currentDateTime = DateTime.UtcNow;
        
        using var connection = context.CreateConnection();

        var newContentId = await connection.ExecuteScalarAsync<int>("""
            INSERT INTO Content (date_created, date_updated, title, description, content_type)
            VALUES (@currentDateTime, null, @title, @description, @contentType)
            RETURNING Id
        """, new
        {
            currentDateTime,
            title = request.Title, 
            description = request.Description, 
            contentType = request.ContentType
        });

        return newContentId;
    }

    public async Task<int> Update(int id, string? title, string? description)
    {
        using var connection = context.CreateConnection();

        var rowsAffected = await connection.ExecuteAsync("""
            UPDATE Content
            SET 
                date_updated = now(),
                title = coalesce(@title, title),
                description = coalesce(@description, description)
            WHERE id = @id
        """, new { id, title, description });

        return rowsAffected;   
    }

    public async Task<int> Delete(int id)
    {
        const string sql = "DELETE FROM Content WHERE id = @id";
        
        using var connection = context.CreateConnection();
        
        var rowsAffected = await connection.ExecuteAsync(sql, new { id });
        
        return rowsAffected;
    }
}