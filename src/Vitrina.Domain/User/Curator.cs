namespace Vitrina.Domain.User;

public class Curator : NotStudentBase
{
    /// <summary>
    ///     User-curated projects.
    /// </summary>
    public ICollection<Project.Project>? Projects { get; init; } = new List<Project.Project>();
}
