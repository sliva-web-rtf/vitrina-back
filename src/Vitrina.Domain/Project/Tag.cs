using System.ComponentModel.DataAnnotations;

namespace Vitrina.Domain.Project;

/// <summary>
///     Project tag.
/// </summary>
public class Tag
{
    /// <summary>
    ///     Tag id.
    /// </summary>
    [Key]
    public int Id { get; private set; }

    /// <summary>
    ///     Tag name.
    /// </summary>
    required public string Name { get; set; }

    /// <summary>
    ///     Tag projects.
    /// </summary>
    public virtual ICollection<Project> Projects { get; private set; } = new List<Project>();
}
