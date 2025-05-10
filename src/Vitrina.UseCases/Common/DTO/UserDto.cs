using Vitrina.Domain.User;

namespace Vitrina.UseCases.Common.DTO;

/// <summary>
///     User dto.
/// </summary>
public class UserDto
{
    /// <summary>
    ///     User role on the platform.
    /// </summary>
    public RoleOnPlatformEnum RoleOnPlatform { get; init; }

    /// <summary>
    ///     User first name.
    /// </summary>
    public string? FirstName { get; init; }

    /// <summary>
    ///     User last name.
    /// </summary>
    public string? LastName { get; init; }

    /// <summary>
    ///     User patronymic.
    /// </summary>
    public string? Patronymic { get; init; }

    /// <summary>
    ///     Telegram username of user.
    /// </summary>
    public string? Telegram { get; init; }

    /// <summary>
    ///     User email.
    /// </summary>
    public string? Email { get; init; }

    /// <summary>
    ///     User phone number.
    /// </summary>
    public string? PhoneNumber { get; init; }
}
