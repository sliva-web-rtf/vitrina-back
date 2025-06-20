using Microsoft.AspNetCore.Identity;
using Vitrina.Domain.Project;

namespace Vitrina.Domain.User;

/// <summary>
/// Custom application user entity.
/// </summary>
public class User : IdentityUser<int>
{
    /// <summary>
    /// The date when user last logged in.
    /// </summary>
    public DateTime? LastLogin { get; set; }

    /// <summary>
    /// Last token reset date. Before the date all generate login tokens are considered
    /// not valid. Must be in UTC format.
    /// </summary>
    public DateTime LastTokenResetAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Indicates when the user was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Indicates when the user was updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Indicates when the user was removed.
    /// </summary>
    public DateTime? RemovedAt { get; set; }

    /// <summary>
    /// Education level of user.
    /// </summary>
    required public string EducationLevel { get; set; }

    /// <summary>
    /// Education course of user.
    /// </summary>
    public int EducationCourse { get; set; }

    /// <summary>
    /// User role in team.
    /// </summary>
    public RoleInTeamEnum RoleInTeam { get; set; }

    /// <summary>
    /// User first name.
    /// </summary>
    required public string FirstName { get; set; }

    /// <summary>
    /// User last name.
    /// </summary>
    required public string LastName { get; set; }

    /// <summary>
    /// User surname.
    /// </summary>
    required public string Surname { get; set; }

    /// <summary>
    /// Full name of user.
    /// </summary>
    public string FullName => $"{LastName} {FirstName} {Surname}";
}
