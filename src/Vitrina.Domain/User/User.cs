using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using Vitrina.Domain.Project.Page;
using Vitrina.Domain.Project.Teammate;

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
    ///     User role on the platform.
    /// </summary>
    public RoleOnPlatformEnum RoleOnPlatform { get; set; }

    /// <summary>
    ///     User first name.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    ///     User last name.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    ///     User patronymic.
    /// </summary>
    public string Patronymic { get; set; }

    /// <summary>
    ///     Full name of user.
    /// </summary>
    public string FullName => $"{LastName} {FirstName} {Patronymic}";

    /// <summary>
    ///     Telegram username of user.
    /// </summary>
    public string Telegram { get; set; }

    /// <summary>
    ///     User's email address.
    /// </summary>
    public override string Email { get; set; }

    /// <summary>
    ///     User phone number.
    /// </summary>
    public override string PhoneNumber { get; set; }

    /// <summary>
    ///     Link to the image in the cloud storage.
    /// </summary>
    public string? Avatar { get; set; }

    /// <summary>
    ///     Positions in teams.
    /// </summary>
    public virtual ICollection<Teammate> PositionsInTeams { get; init; } = new List<Teammate>();

    /// <summary>
    ///     The list of project pages available for editing.
    /// </summary>
    public virtual ICollection<PageEditor> EditingRights { get; init; } = new List<PageEditor>();

    /// <summary>
    ///     User profile information.
    /// </summary>
    [Column(TypeName = "jsonb")]
    public string ProfileData { get; set; }

    public static User CreteUser(
        string lastName,
        string firstName,
        string patronymic,
        RoleOnPlatformEnum roleOnPlatform,
        string email,
        bool emailConfirmed = false,
        int educationCourse = -1,
        EducationLevelEnum educationLevel = EducationLevelEnum.NotStudent)
    {
        var user = new User();
        var json = new JObject();
        user.UserName = email;
        user.EmailConfirmed = emailConfirmed;

        json["email"] = user.Email = email;
        json["firstName"] = user.FirstName = firstName;
        json["lastName"] = user.LastName = lastName;
        json["patronymic"] = user.Patronymic = patronymic;
        json["roleOnPlatform"] = (user.RoleOnPlatform = roleOnPlatform).ToString();

        if (roleOnPlatform == RoleOnPlatformEnum.Student)
        {
            json["educationCourse"] = educationCourse;
            json["educationLevel"] = educationLevel.ToString();
        }

        user.ProfileData = json.ToString();

        return user;
    }
}
