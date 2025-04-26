using Vitrina.Domain.Project.Page.Blocks;

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
    ///     Collection of text content blocks.
    /// </summary>
    public virtual ICollection<TextBlock> TextBlocks { get; init; } = new List<TextBlock>();

    /// <summary>
    ///     Collection of blocks with information about the team.
    /// </summary>
    public virtual ICollection<CommandBlock> CommandBlocks { get; init; } = new List<CommandBlock>();

    /// <summary>
    ///     Collection of blocks with image and text.
    /// </summary>
    public virtual ICollection<BlockWithTextsAndImages> BlocksWithTextsAndImages { get; init; } =
        new List<BlockWithTextsAndImages>();

    public virtual ICollection<BlockWithTextAndImage> BlocksWithTextAndImages { get; init; } = new List<BlockWithTextAndImage>();

    /// <summary>
    ///     Collection of blocks with images.
    /// </summary>
    public virtual ICollection<ImageBlock> ImageBlocks { get; init; } = new List<ImageBlock>();

    /// <summary>
    ///     A collection of image carousel blocks.
    /// </summary>
    public virtual ICollection<ImageCarouselBlock> ImageCarouselBlocks { get; init; } = new List<ImageCarouselBlock>();

    /// <summary>
    ///     Collection of horizontal divider blocks.
    /// </summary>
    public virtual ICollection<HorizontalDividerBlock> HorizontalDividerBlocks { get; init; } =
        new List<HorizontalDividerBlock>();

    /// <summary>
    ///     Collection of code blocks.
    /// </summary>
    public virtual ICollection<CodeBlock> CodeBlocks { get; init; } = new List<CodeBlock>();

    /// <summary>
    ///     A collection of video content blocks.
    /// </summary>
    public virtual ICollection<VideoBlock> VideoBlocks { get; init; } = new List<VideoBlock>();

    /// <summary>
    ///     Project id.
    /// </summary>
    public int ProjectId { get; init; }

    /// <summary>
    ///     Project.
    /// </summary>
    public Project? Project { get; init; }
}
