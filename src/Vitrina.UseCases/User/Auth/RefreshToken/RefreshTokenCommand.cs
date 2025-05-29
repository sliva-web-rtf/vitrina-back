using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Vitrina.UseCases.User.Auth.RefreshToken;

/// <summary>
///     Refresh token command.
/// </summary>
public record RefreshTokenCommand : IRequest<TokenModel>
{
    /// <summary>
    ///     User token.
    /// </summary>
    [Required]
    required public string Token { get; init; }
}
