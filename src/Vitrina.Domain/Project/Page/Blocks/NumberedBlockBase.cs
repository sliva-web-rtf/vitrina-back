namespace Vitrina.Domain.Project.Page.Blocks;

/// <summary>
///     Numbered content unit.
/// </summary>
public abstract class NumberedBlockBase : BaseEntity<Guid>
{
    /// <summary>
    ///     The element that the content block belongs to.
    /// </summary>
    public ProjectPage ProjectPage { get; set; }

    /// <summary>
    ///     A foreign key for the content that this block belongs to.
    /// </summary>
    public Guid ProjectPageId { get; set; }

    /// <summary>
    ///     Serial number on the design canvas.
    /// </summary>
    public int NumberOnCanvas { get; set; }
}
