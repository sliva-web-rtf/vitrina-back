using MediatR;
using Vitrina.UseCases.User.DTO;

namespace Vitrina.UseCases.Auth.GetUserById;

/// <summary>
///     Get user details by identifier.
/// </summary>
public record GetUserByIdQuery : IRequest<UserDetailsDto>
{
    /// <summary>
    ///     User id.
    /// </summary>
    public int UserId { get; init; }
}
