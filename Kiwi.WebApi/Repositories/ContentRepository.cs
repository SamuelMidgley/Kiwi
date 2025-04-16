using Dapper;
using Kiwi.WebApi.Helpers;
using Kiwi.WebApi.Models.Content;
using Kiwi.WebApi.Repositories.Interfaces;

namespace Kiwi.WebApi.Repositories;

public class ContentRepository(DataContext context, ILogger<ContentRepository> logger) 
    : IContentRepository
{
    public async Task<IEnumerable<Content>> GetContent()
    {
        try
        {
            using var connection = context.CreateConnection();
        
            var content = await connection.QueryAsync<Content>("""
               SELECT *
               From Content
            """);
        
            return content;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to get content");
            throw;
        }
    }

    public async Task<Content?> GetContentById(int id)
    {
        using var connection = context.CreateConnection();
        
        var content = await connection.QueryFirstOrDefaultAsync<Content>("""
           SELECT *
           From Content
           WHERE Id = @id
        """, new { id });
        
        return content;    
    }

    public async Task<int> CreateContent(CreateContentRequest request)
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

    public async Task<bool> UpdateContent(int id, UpdateContentRequest request)
    {
        using var connection = context.CreateConnection();

        var rowsAffected = await connection.ExecuteAsync("""
            UPDATE Content
            SET 
                date_updated = now(),
                title = coalesce(@title, title),
                description = coalesce(@description, description)
            WHERE id = @id
        """, new
        {
            id,
            title = request.Title,
            description = request.Description
        });

        return rowsAffected == 1;    
    }

    public async Task<bool> DeleteContent(int id)
    {
        using var connection = context.CreateConnection();

        var rowsAffected = await connection.ExecuteAsync("""
            DELETE FROM Content 
            WHERE id = @id
        """);
        
        return rowsAffected == 1;
    }
}