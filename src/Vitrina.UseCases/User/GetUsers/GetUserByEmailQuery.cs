using MediatR;
using Saritasa.Tools.Common.Pagination;
using Vitrina.UseCases.Common.Pagination;
using Vitrina.UseCases.User.DTO;

namespace Vitrina.UseCases.User.GetUsers;

/// <summary>
///     Query for user search.
/// </summary>
public record GetUserByEmailQuery(string Email) : PageQueryFilter, IRequest<ResponceShortenedUserDto>;
