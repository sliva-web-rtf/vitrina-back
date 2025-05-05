using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Vitrina.Domain.User;

/// <summary>
///     Custom application user entity.
/// </summary>
public class User : IdentityUser<int>
{
    /// <summary>
    ///     User registration status.
    /// </summary>
    public RegistrationStatusEnum RegistrationStatus { get; set; }

    /// <summary>
    ///     The date when user last logged in.
    /// </summary>
    public DateTime? LastLogin { get; set; }

    /// <summary>
    ///     Last token reset date. Before the date all generate login tokens are considered
    ///     not valid. Must be in UTC format.
    /// </summary>
    public DateTime LastTokenResetAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    ///     Indicates when the user was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    ///     Indicates when the user was updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    ///     Indicates when the user was removed.
    /// </summary>
    public DateTime? RemovedAt { get; set; }

    /// <summary>
    ///     User first name.
    /// </summary>
    required public string FirstName { get; set; }

    /// <summary>
    ///     User last name.
    /// </summary>
    required public string LastName { get; set; }

    /// <summary>
    ///     User patronymic.
    /// </summary>
    required public string Patronymic { get; set; }

    /// <summary>
    ///     Full name of user.
    /// </summary>
    public string FullName => $"{LastName} {FirstName} {Patronymic}";

    /// <summary>
    ///     Telegram username of user.
    /// </summary>
    public string? Telegram { get; set; }

    /// <summary>
    ///     User's email address.
    /// </summary>
    required public override string Email { get; set; }

    /// <summary>
    ///     User phone number.
    /// </summary>
    public override string? PhoneNumber { get; set; }

    /// <summary>
    ///     Link to the image in the cloud storage.
    /// </summary>
    public string? Avatar { get; set; }

    /// <summary>
    ///     User role on the platform.
    /// </summary>
    public RoleOnPlatformEnum RoleOnPlatform { get; set; }

    [Column(TypeName = "jsonb")] public string? AdditionalInformation { get; set; }
}
