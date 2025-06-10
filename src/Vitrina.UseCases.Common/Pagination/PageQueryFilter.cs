using System.ComponentModel.DataAnnotations;

namespace Vitrina.UseCases.Common.Pagination;

/// <summary>
///     Base class for filters that contains page, page size and sorting entries.
/// </summary>
public abstract record PageQueryFilter
{
    /// <summary>
    ///     Page number to return. Starts with 1.
    /// </summary>
    [Range(1, int.MaxValue)]
    public int Page { get; init; } = 1;

    /// <summary>
    ///     Required page size (amount of items returned at a time).
    /// </summary>
    [Range(1, 1000)]
    public int PageSize { get; init; } = 100;
}
