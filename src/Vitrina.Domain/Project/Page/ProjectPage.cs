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
    public virtual ICollection<ContentBlock> ContentBlocks { get; private set; } = new List<ContentBlock>();

    /// <summary>
    ///     Project id.
    /// </summary>
    public Guid ProjectId { get; init; }

    /// <summary>
    ///     Project.
    /// </summary>
    public Project? Project { get; init; }

    public void NumberCustomBlocks() =>
        ContentBlocks = ContentBlocks
            .Select((block, index) =>
            {
                block.NumberOnPage = index;
                return block;
            })
            .ToList();

    public void SortContentBlocks() =>
        ContentBlocks = ContentBlocks
            .OrderBy(contentBlock => contentBlock.NumberOnPage)
            .ToList();
}
