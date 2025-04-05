using Vitrina.Domain.User;
using Vitrina.UseCases.Specialization;
using Vitrina.UseCases.User.DTO.Profile.Base;

namespace Vitrina.UseCases.User.DTO.Profile;

public class UpdateStudentDto : UserDtoBase
{
    /// <summary>
    /// Education level  of student.
    /// </summary>
    public EducationLevelEnum EducationLevel { get; init; }

    /// <summary>
    /// Education course of student.
    /// </summary>
    public int EducationCourse { get; init; }

    /// <summary>
    /// Link to the image in the cloud storage.
    /// </summary>
    public string? Resume { get; init; }

    /// <summary>
    /// Roles in the team.
    /// </summary>
    public ICollection<string>? RolesInTeam { get; init; }

    /// <summary>
    /// Student specializations.
    /// </summary>
    public SpecializationDto? Specialization { get; init; }
}
