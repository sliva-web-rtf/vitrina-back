using Vitrina.Domain.Project.Page;
using Vitrina.Domain.Project.Page.Blocks;

namespace Vitrina.UseCases.ProjectPages;

public record ProjectPageDto
{
    /// <summary>
    ///     Page status.
    /// </summary>
    public PageReadyStatusEnum ReadyStatus { get; init; }

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
    public virtual ICollection<BlockWithTextsAndPictures> BlocksWithTextsAndPictures { get; init; } =
        new List<BlockWithTextsAndPictures>();

    /// <summary>
    ///     Collection of blocks with images.
    /// </summary>
    public virtual ICollection<ImageBlock> ImageBlocks { get; init; } = new List<ImageBlock>();

    /// <summary>
    ///     A collection of image carousel blocks.
    /// </summary>
    public virtual ICollection<CarouselImages> ImageCarouselBlocks { get; init; } = new List<CarouselImages>();

    /// <summary>
    ///     Collection of horizontal divider blocks.
    /// </summary>
    public virtual ICollection<HorizontalDivider> HorizontalDividerBlocks { get; init; } =
        new List<HorizontalDivider>();

    /// <summary>
    ///     Collection of code blocks.
    /// </summary>
    public virtual ICollection<СodeBlock> CodeBlocks { get; init; } = new List<СodeBlock>();

    /// <summary>
    ///     A collection of video content blocks.
    /// </summary>
    public virtual ICollection<VideoBlock> VideoBlocks { get; init; } = new List<VideoBlock>();

    /// <summary>
    ///     Project id.
    /// </summary>
    public int ProjectId { get; init; }

    /// <summary>
    ///     User ID who created the project page.
    /// </summary>
    public int CreatorId { get; init; }
}
