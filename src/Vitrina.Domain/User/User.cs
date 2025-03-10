using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;

namespace Vitrina.Domain.User;

/// <summary>
/// Custom application user entity.
/// </summary>
public class User : IdentityUser<int>
{
    /// <summary>
    /// User registration status.
    /// </summary>
    public RegistrationStatusEnum RegistrationStatus;

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

    private EducationLevelEnum? educationLevel;

    /// <summary>
    /// Education level of user.
    /// </summary>
    public EducationLevelEnum? EducationLevel
    {
        get => educationLevel;
        set
        {
            educationLevel = value;
            if (RoleOnPlatform == RoleOnPlatformEnum.Student)
            {
                ProfileData["educationLevel"] = $"{educationLevel}";
            }
        }
    }

    private int? educationCourse;

    /// <summary>
    /// Education course of user.
    /// </summary>
    public int? EducationCourse
    {
        get => educationCourse;
        set
        {
            educationCourse = value;
            if (RoleOnPlatform == RoleOnPlatformEnum.Student)
            {
                ProfileData["educationCourse"] = educationCourse;
            }
        }
    }

    private RoleOnPlatformEnum roleOnPlatform;

    /// <summary>
    /// User role on the platform.
    /// </summary>
    required public RoleOnPlatformEnum RoleOnPlatform
    {
        get => roleOnPlatform;
        set
        {
            roleOnPlatform = value;
            ProfileData["roleOnPlatform"] = $"{roleOnPlatform}";
        }
    }

    private string firstName;

    /// <summary>
    /// User first name.
    /// </summary>
    required public string FirstName
    {
        get => firstName;
        set
        {
            firstName = value;
            ProfileData["firstName"] = value;
        }
    }

    private string lastName;

    /// <summary>
    /// User last name.
    /// </summary>
    required public string LastName
    {
        get => lastName;
        set
        {
            lastName = value;
            ProfileData["lastName"] = value;
        }
    }

    private string patronymic;

    /// <summary>
    /// User patronymic.
    /// </summary>
    required public string Patronymic
    {
        get => patronymic;
        set
        {
            patronymic = value;
            ProfileData["patronymic"] = value;
        }
    }

    /// <summary>
    /// Full name of user.
    /// </summary>
    public string FullName => $"{LastName} {FirstName} {Patronymic}";

    private string? telegram;

    /// <summary>
    /// Telegram username of user.
    /// </summary>
    public string? Telegram
    {
        get => telegram;
        set
        {
            telegram = value;
            ProfileData["telegram"] = value;
        }
    }

    private string? email;

    /// <summary>
    /// User's email address.
    /// </summary>
    public override string? Email
    {
        get => email;
        set
        {
            email = value;
            ProfileData["email"] = email;
        }
    }

    private string? phoneNumber;

    /// <summary>
    /// User phone number.
    /// </summary>
    public override string? PhoneNumber
    {
        get => phoneNumber;
        set
        {
            phoneNumber = value;
            ProfileData["phoneNumber"] = phoneNumber;
        }
    }

    private string? resume;

    /// <summary>
    /// Link to the resume pdf file in the cloud storage.
    /// </summary>
    public string? Resume
    {
        get => resume;
        set
        {
            resume = value;
            if (RoleOnPlatform == RoleOnPlatformEnum.Student)
            {
                ProfileData["resume"] = resume;
            }
        }
    }

    private string? post;

    /// <summary>
    /// Position in the company.
    /// </summary>
    public string? Post
    {
        get => post;
        set
        {
            post = value;
            if (RoleOnPlatform != RoleOnPlatformEnum.Student)
            {
                ProfileData["post"] = post;
            }
        }
    }

    private string? company;

    /// <summary>
    /// Place of work
    /// </summary>
    public string? Company
    {
        get => company;
        set
        {
            company = value;
            if (RoleOnPlatform != RoleOnPlatformEnum.Student)
            {
                ProfileData["company"] = company;
            }
        }
    }

    /// <summary>
    /// User profile data, depending on their role on the platform
    /// </summary>
    [Column(TypeName = "jsonb")]
    public JObject ProfileData { get; set; }

    private ICollection<Specialization>? specializations;

    /// <summary>
    /// User specializations.
    /// </summary>
    public ICollection<Specialization>? Specializations
    {
        get => specializations;
        set
        {
            specializations = value;
            if (RoleOnPlatform == RoleOnPlatformEnum.Student)
            {
                ProfileData["specializations"] = JArray.FromObject(specializations ?? new Specialization[0]);
            }
        }
    }

    /// <summary>
    /// Link to the image in the cloud storage.
    /// </summary>
    public string? Avatar { get; set; }

    private List<string> rolesInTeam;

    /// <summary>
    /// Role in the team.
    /// </summary>
    public List<string> RolesInTeam
    {
        get => rolesInTeam;
        set
        {
            rolesInTeam = value;
            if (RoleOnPlatform == RoleOnPlatformEnum.Student)
            {
                ProfileData["rolesInTeam"] = JArray.FromObject(rolesInTeam ?? new List<string>());
            }
        }
    }
}
