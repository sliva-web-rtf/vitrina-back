namespace Vitrina.Domain.Project.Teammate;

/// <summary>
///     Project users.
/// </summary>
public class Teammate : BaseEntity<int>
{
    /// <summary>
    ///     User id.
    /// </summary>
    public int UserId { get; init; }

    /// <summary>
    ///     User.
    /// </summary>
    public virtual User.User User { get; set; }

    public Guid TeamId { get; set; }

    required public virtual Team Team { get; set; }

    /// <summary>
    ///     User roles.
    /// </summary>
    public virtual ICollection<ProjectRole> Roles { get; set; } = new List<ProjectRole>();
}
