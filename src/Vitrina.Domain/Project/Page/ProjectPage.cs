namespace Vitrina.Domain.Project.Page;

/// <inheritdoc />
public class ProjectPage : BaseEntity<Guid>
{
    /// <summary>
    ///     Users who can edit the page.
    /// </summary>
    public virtual ICollection<PageEditor> Editors { get; init; } = new List<PageEditor>();

    /// <summary>
    ///     Page status.
    /// </summary>
    public PageReadyStatusEnum ReadyStatus { get; set; }

    /// <summary>
    ///     Page content blocks.
    /// </summary>
    public virtual ICollection<ContentBlock> ContentBlocks { get; init; } = new List<ContentBlock>();

    /// <summary>
    ///     Project id.
    /// </summary>
    public int ProjectId { get; init; }

    /// <summary>
    ///     Project.
    /// </summary>
    public Project? Project { get; init; }
}
