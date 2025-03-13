using Microsoft.AspNetCore.Identity;
using Vitrina.Domain.Project;

namespace Vitrina.Domain.User;

/// <summary>
/// Custom application user entity.
/// </summary>
public class User : IdentityUser<int>
{
    /// <summary>
    /// User registration status.
    /// </summary>
    public RegistrationStatusEnum RegistrationStatus { get; set; }

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
    public EducationLevelEnum EducationLevel { get; set; }

    /// <summary>
    /// Education course of user.
    /// </summary>
    public int? EducationCourse { get; set; }

    /// <summary>
    /// User role on the platform.
    /// </summary>
    required public RoleOnPlatformEnum RoleOnPlatform { get; set; }

    /// <summary>
    /// User first name.
    /// </summary>
    required public string FirstName { get; set; }

    /// <summary>
    /// User last name.
    /// </summary>
    required public string LastName { get; set; }

    /// <summary>
    /// User patronymic.
    /// </summary>
    required public string Patronymic { get; set; }

    /// <summary>
    /// Full name of user.
    /// </summary>
    public string FullName => $"{LastName} {FirstName} {Patronymic}";

    /// <summary>
    /// Telegram username of user.
    /// </summary>
    public string Telegram { get; set; }

    /// <summary>
    /// User's email address.
    /// </summary>
    public override string Email { get; set; }

    /// <summary>
    /// User phone number.
    /// </summary>
    public override string PhoneNumber { get; set; }

    /// <summary>
    /// Link to the resume pdf file in the cloud storage.
    /// </summary>
    public string? Resume { get; set; }

    /// <summary>
    /// Position in the company.
    /// </summary>
    public string? Post { get; set; }

    /// <summary>
    /// Place of work
    /// </summary>
    public string? Company { get; set; }

    /// <summary>
    /// User specializations.
    /// </summary>
    public ICollection<Specialization> Specializations { get; set; } = new List<Specialization>();

    /// <summary>
    /// Link to the image in the cloud storage.
    /// </summary>
    public string? Avatar { get; set; }

    /// <summary>
    /// Role in the team.
    /// </summary>
    public List<string>? RolesInTeam { get; set; }

    /// <summary>
    /// Positions in teams
    /// </summary>
    public ICollection<Teammate> PositionsInTeams { get; init; } = new List<Teammate>();
}
