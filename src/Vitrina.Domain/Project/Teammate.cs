using Vitrina.Domain.User;

namespace Vitrina.Domain.Project;

/// <summary>
///     Project users.
/// </summary>
public class Teammate : BaseEntity<int>
{
    /// <summary>
    ///     Student id.
    /// </summary>
    public int StudentId { get; init; }

    /// <summary>
    ///     User.
    /// </summary>
    public virtual Student Student { get; private set; }

    /// <summary>
    ///     User project id.
    /// </summary>
    public int ProjectId { get; set; }

    /// <summary>
    ///     User project.
    /// </summary>
    required public virtual Project Project { get; set; }

    /// <summary>
    ///     User roles.
    /// </summary>
    public virtual ICollection<ProjectRole> Roles { get; set; } = new List<ProjectRole>();
}
