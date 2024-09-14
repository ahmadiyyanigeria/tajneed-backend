using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TajneedApi.Application.Wrappers.Paging;
/// <summary>
/// Represents the request parameters for pagination, sorting, and searching in data retrieval operations.
/// </summary>
public record PageRequest
{
    /// <summary>
    /// Gets the number of items to include in a page of results.
    /// Default value is 10.
    /// </summary>
    public int PageSize { get; init; } = 10;

    /// <summary>
    /// Gets the current page number.
    /// Default value is 1, which represents the first page.
    /// </summary>
    public int Page { get; init; } = 1;

    /// <summary>
    /// Gets the name of the property to sort the results by.
    /// Optional field. If not specified, no sorting is applied.
    /// </summary>
    public string? SortBy { get; init; }

    /// <summary>
    /// Gets the direction to sort the results by.
    /// Can be either "asc" for ascending or "desc" for descending.
    /// Default value is "asc".
    /// </summary>
    [DefaultValue("asc")]
    public string? SortDirection { get; init; }

    /// <summary>
    /// Gets the keyword to filter or search through the results.
    /// Optional field. If not specified, no filtering is applied.
    /// </summary>
    public string? Keyword { get; init; }

    /// <summary>
    /// Gets a value indicating whether the results should be sorted in ascending order.
    /// Default value is true (ascending order).
    /// </summary>
    public bool IsAscending { get; init; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether pagination should be applied.
    /// Default value is true, meaning pagination is applied.
    /// If set to false, all results are returned without pagination.
    /// </summary>
    public bool UsePaging { get; set; } = true;
}
