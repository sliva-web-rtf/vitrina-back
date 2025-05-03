using Vitrina.Domain.Project;

namespace Vitrina.Domain.User;

public class Student : UserWithRoleBase
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
    public string? Resume { get; init; }

    /// <summary>
    ///     Roles in the team.
    /// </summary>
    public ICollection<string>? RolesInTeam { get; init; }

    /// <summary>
    ///     Student specializations.
    /// </summary>
    public Specialization? Specialization { get; init; }

    /// <summary>
    ///     Positions in teams.
    /// </summary>
    public virtual ICollection<Teammate> PositionsInTeams { get; init; } = new List<Teammate>();
}
