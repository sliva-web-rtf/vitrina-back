using Vitrina.Domain.User;
using Vitrina.UseCases.UserSpecialization;

namespace Vitrina.UseCases.User.DTO.AdditionalInformation;

public class AdditionalStudentInfo
{
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
    public Guid? ResumeId { get; init; }

    /// <summary>
    ///     Role in the team.
    /// </summary>
    public string? RoleInTeam { get; init; }

    /// <summary>
    ///     Student specializations.
    /// </summary>
    public SpecializationDto? Specialization { get; init; }
}
