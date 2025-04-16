using Dapper;
using Kiwi.Core.Entities.Content;
using Kiwi.Core.Interfaces.Content;

namespace Kiwi.Infrastructure.Data;

public class ContentRepository(DataContext context) : IContentRepository
{
    public async Task<int> CreateContent(Content request)
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
}