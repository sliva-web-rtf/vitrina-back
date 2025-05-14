using Vitrina.Domain.Project.Page;

namespace Vitrina.Domain.Project;

/// <summary>
/// Domain class of project.
/// </summary>
public class Project : BaseEntity<int>
{
    /// <summary>
    /// Project name.
    /// </summary>
    required public string Name { get; set; }

    /// <summary>
    ///     Project description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    ///     Path to preview image.
    /// </summary>
    public string? PreviewImagePath { get; set; }

    /// <summary>
    /// Project client.
    /// </summary>
    public string? Client { get; set; }

    /// <summary>
    ///     Project page id.
    /// </summary>
    required public Guid PageId { get; init; }

    /// <summary>
    ///     Project page content.
    /// </summary>
    required public virtual ProjectPage Page { get; init; }

    /// <summary>
    ///     Priority of project.
    /// </summary>
    public int Priority { get; set; }

    /// <summary>
    ///     Project team.
    /// </summary>
    public virtual ICollection<Teammate.Teammate> TeamMembers { get; set; } = new List<Teammate.Teammate>();

    /// <summary>
    ///     Project sphere.
    /// </summary>
    public string? Sphere { get; set; }

    /// <summary>
    ///     Project type.
    /// </summary>
    public string? Type { get; set; }
}
