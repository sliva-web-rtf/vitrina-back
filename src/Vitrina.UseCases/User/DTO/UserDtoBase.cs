using Vitrina.Domain.User;
using Vitrina.UseCases.User.DTO;

namespace Vitrina.UseCases.Common.DTO;

/// <summary>
///     User dto.
/// </summary>
public record UserDtoBase : ResponceShortenedUserDto
{
    /// <summary>
    ///     User role on the platform.
    /// </summary>
    public RoleOnPlatformEnum RoleOnPlatform { get; init; }

    /// <summary>
    ///     Telegram username of user.
    /// </summary>
    public string? Telegram { get; init; }

    /// <summary>
    ///     User phone number.
    /// </summary>
    public string? PhoneNumber { get; init; }
}
