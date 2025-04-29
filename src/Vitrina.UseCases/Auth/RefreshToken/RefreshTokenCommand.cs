using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Vitrina.UseCases.Auth.RefreshToken;

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
