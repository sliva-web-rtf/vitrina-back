using System.ComponentModel.DataAnnotations;
using MediatR;
using Saritasa.Tools.Common.Pagination;
using Vitrina.UseCases.User.DTO;

namespace Vitrina.UseCases.User.GetUsers;

/// <summary>
///     Query for user search.
/// </summary>
public record GetUsersQuery : IRequest<PagedList<ResponceShortenedUserDto>>
{
    public string? Email { get; init; }

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
