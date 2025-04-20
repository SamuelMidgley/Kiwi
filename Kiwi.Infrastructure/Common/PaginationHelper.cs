using System.Data;
using Kiwi.Application.Common.Models;

namespace Kiwi.Infrastructure.Common;

public static class PaginationHelper
{
    public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(
        this IDbConnection connection,
        string sqlBaseQuery,
        object? parameters,
        int pageNumber,
        int pageSize)
    {
        var offset = (pageNumber - 1) * pageSize;

        var countQuery = $"SELECT COUNT(*) FROM ({sqlBaseQuery}) AS count_alias";

        var totalCount = await connection.ExecuteScalarAsync<int>(countQuery, parameters);

        var paginatedQuery = $"""
                                  {sqlBaseQuery}
                                  LIMIT @pageSize OFFSET @offset
                              """;

        var paginatedItems = await connection.QueryAsync<T>(
            paginatedQuery, new { pageSize, offset });

        return new PaginatedList<T>(paginatedItems.ToList(), totalCount, pageNumber, pageSize);
    }
}