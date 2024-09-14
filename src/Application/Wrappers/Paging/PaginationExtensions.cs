namespace TajneedApi.Application.Wrappers.Paging;

public static class PaginationExtensions
{
    public static PaginatedList<T> ToPaginatedList<T>(this IQueryable<T> query, int page, int pageSize) where T : notnull
    {
        var totalItems = query.Count();
        int offset = (page - 1) * pageSize;
        var items = query.Skip(offset).Take(pageSize);
        return new() { Items = items, TotalItems = totalItems, Page = page, PageSize = pageSize };
    }
}
