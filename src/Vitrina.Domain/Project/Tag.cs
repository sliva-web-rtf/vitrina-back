namespace Vitrina.Domain.Project;

/// <summary>
///     Project tag.
/// </summary>
public class Tag : BaseEntity<int>
{
    /// <summary>
    ///     Tag name.
    /// </summary>
    required public string Name { get; set; }

    /// <summary>
    ///     Tag projects.
    /// </summary>
    public virtual ICollection<Project> Projects { get; private set; } = new List<Project>();
}
