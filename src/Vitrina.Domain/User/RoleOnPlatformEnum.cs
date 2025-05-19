namespace Vitrina.Domain.User;

/// <summary>
///     Types of roles for user.
/// </summary>
[Flags]
public enum RoleOnPlatformEnum
{
    /// <summary>
    ///     Student role.
    /// </summary>
    Student = 1,

    /// <summary>
    ///     Curator role.
    /// </summary>
    Curator,

    /// <summary>
    ///     Partner role.
    /// </summary>
    Partner,

    Administrator
}
