namespace Vitrina.UseCases.Common;

/// <summary>
/// User dto.
/// </summary>
public class UserDto
{
    /// <summary>
    /// User email.
    /// </summary>
    public string? Email { get; init; }

    /// <summary>
    /// User name.
    /// </summary>
    required public string FirstName { get; init; }

    /// <summary>
    /// User last name.
    /// </summary>
    required public string LastName { get; init; }

    /// <summary>
    /// User patronymic.
    /// </summary>
    public string? Patronymic { get; init; }

    /// <summary>
    /// User avatar.
    /// </summary>
    public ICollection<byte>? Avatar { get; init; } = new List<byte>();

    /// <summary>
    /// User roles.
    /// </summary>
    public ICollection<RoleDto> Roles { get; init; } = new List<RoleDto>();
}
