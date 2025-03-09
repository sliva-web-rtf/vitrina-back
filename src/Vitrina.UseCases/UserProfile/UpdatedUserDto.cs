using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Vitrina.UseCases.Common;

namespace Vitrina.Domain.User;

/// <summary>
/// DTO for updating user data.
/// </summary>
public record UpdatedUserDto
{
    /// <summary>
    /// Education level  of user.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public EducationLevelEnum? EducationLevel { get; init; }

    /// <summary>
    /// Education course of user.
    /// </summary>
    public int? EducationCourse { get; init; }

    /// <summary>
    /// User role in team.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public RoleOnPlatformEnum? RoleOnPlatform { get; init; }

    /// <summary>
    /// User first name.
    /// </summary>
    [StringLength(255, ErrorMessage = "The FirstName must be no more than 255 characters long.")]
    [RegularExpression(@"^(?!.*\d).+$", ErrorMessage = "The FirstName must not contain numbers and must not be empty.")]
    public string? FirstName { get; init; }

    /// <summary>
    /// User last name.
    /// </summary>
    [StringLength(255, ErrorMessage = "The LastName must be no more than 255 characters long.")]
    [RegularExpression(@"^(?!.*\d).+$", ErrorMessage = "The LastName must not contain numbers and must not be empty.")]
    public string? LastName { get; init; }

    /// <summary>
    /// User patronymic.
    /// </summary>
    [StringLength(255, ErrorMessage = "The Patronymic must be no more than 255 characters long.")]
    [RegularExpression(@"^(?!.*\d).+$", ErrorMessage = "The Patronymic must not contain numbers and must not be empty.")]
    public string? Patronymic { get; init; }

    /// <summary>
    /// Telegram username of user.
    /// </summary>
    [RegularExpression("^[a-zA-Z][a-zA-Z0-9_-]{4,31}$", ErrorMessage = "This is an incorrect Telegram username.")]
    public string? Telegram { get; init; }

    /// <summary>
    /// User email.
    /// </summary>
    [EmailAddress]
    [StringLength(100, ErrorMessage = "The Email must be no more than 100 characters long.")]
    public string? Email { get; init; }

    /// <summary>
    /// User phone number.
    /// </summary>
    [RegularExpression(@"^\+7\d{10}$", ErrorMessage = "Incorrect phone number.")]
    public string? PhoneNumber { get; init; }

    /// <summary>
    /// Link to the image in the cloud storage.
    /// </summary>
    public string? Resume { get; init; }

    /// <summary>
    /// Position in the company.
    /// </summary>
    [StringLength(255, ErrorMessage = "The Post must be no more than 255 characters long.")]
    public string? Post { get; init; }

    /// <summary>
    /// Place of work
    /// </summary>
    [StringLength(255, ErrorMessage = "The Company must be no more than 255 characters long.")]
    public string? Company { get; init; }

    /// <summary>
    /// User specializations.
    /// </summary>
    public ICollection<SpecializationDto>? Specializations { get; init; }

    /// <summary>
    /// Link to the image in the cloud storage.
    /// </summary>
    public string? Avatar { get; init; }

    /// <summary>
    /// Roles in the team.
    /// </summary>
    public ICollection<string>? RolesInTeam { get; init; }
}
