using Microsoft.EntityFrameworkCore;
using TajneedApi.Application.Repositories.Paging;

namespace TajneedApi.Infrastructure.Extensions;

public static class PaginationExtensions
{
    public static async Task<PaginatedList<T>> ToPaginatedList<T>(this IQueryable<T> query, int page = 1, int pageSize = 10, bool usePaging = true) where T : notnull
    {
        var totalItems = await query.CountAsync();

        if (!usePaging)
            return new() { Items = await query.ToListAsync(), TotalItems = totalItems, Page = page, PageSize = totalItems };

        int offset = (page - 1) * pageSize;
        var items = await query.Skip(offset).Take(pageSize).ToListAsync();
        return new() { Items = items, TotalItems = totalItems, Page = page, PageSize = pageSize };
    }
}
