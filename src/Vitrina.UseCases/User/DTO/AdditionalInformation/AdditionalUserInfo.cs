using System.ComponentModel.DataAnnotations;
using Vitrina.Domain.User;
using Vitrina.UseCases.Specialization;

namespace Vitrina.UseCases.User.DTO.AdditionalInformation;

public class AdditionalUserInfo
{
    /// <summary>
    ///     Place of work
    /// </summary>
    [StringLength(255, ErrorMessage = "The Company must be no more than 255 characters long.")]
    public string? Company { get; init; }

    /// <summary>
    ///     Position in the company.
    /// </summary>
    [StringLength(255, ErrorMessage = "The Post must be no more than 255 characters long.")]
    public string? Post { get; init; }

    /// <summary>
    ///     Education level  of student.
    /// </summary>
    public EducationLevelEnum EducationLevel { get; init; }

    /// <summary>
    ///     Education course of student.
    /// </summary>
    public int EducationCourse { get; init; }

    /// <summary>
    ///     Link to the image in the cloud storage.
    /// </summary>
    public string? Resume { get; init; }

    /// <summary>
    ///     Role in the team.
    /// </summary>
    public string? RoleInTeam { get; init; }

    /// <summary>
    ///     Student specializations.
    /// </summary>
    public SpecializationDto? Specialization { get; init; }
}
