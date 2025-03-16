using System.ComponentModel.DataAnnotations;

namespace Vitrina.Domain.Project;

/// <summary>
/// Project users.
/// </summary>
public class Teammate
{
    /// <summary>
    /// User id.
    /// </summary>
    [Key]
    public int Id { get; private set; }

    /// <summary>
    /// User id.
    /// </summary>
    public int UserId { get; init; }

    /// <summary>
    /// User.
    /// </summary>
    public virtual User.User User { get; private set; }

    /// <summary>
    /// User project id.
    /// </summary>
    public int ProjectId { get; set; }

    /// <summary>
    /// User project.
    /// </summary>
    required public virtual Project Project { get; set; }

    /// <summary>
    /// User roles.
    /// </summary>
    public virtual ICollection<ProjectRole> Roles { get; private set; } = new List<ProjectRole>();
}
