using MediatR;
using Saritasa.Tools.Common.Pagination;
using Vitrina.UseCases.Common.Pagination;
using Vitrina.UseCases.User.DTO;

namespace Vitrina.UseCases.User.GetUsers;

/// <summary>
///     Query for user search.
/// </summary>
public record GetUsersQuery : PageQueryFilter, IRequest<PagedList<ResponceShortenedUserDto>>
{
    public string? Email { get; init; }
}
