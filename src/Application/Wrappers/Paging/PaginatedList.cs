namespace TajneedApi.Application.Wrappers.Paging;

public record PaginatedList<T> where T : notnull
{
    /// <summary>
    /// Gets the collection of items in the current page.
    /// Default is an empty list.
    /// </summary>
    public IEnumerable<T> Items { get; init; } = [];

    /// <summary>
    /// Gets the total number of items across all pages.
    /// </summary>
    public long TotalItems { get; init; }

    /// <summary>
    /// Gets the current page number.
    /// </summary>
    public int Page { get; init; }

    /// <summary>
    /// Gets the number of items per page.
    /// </summary>
    public int PageSize { get; init; }

    /// <summary>
    /// Gets the total number of pages, calculated based on <see cref="TotalItems"/> and <see cref="PageSize"/>.
    /// This value is the ceiling of the division of total items by page size.
    /// </summary>
    public long TotalPages => (long)Math.Ceiling(TotalItems / (double)PageSize);
}
