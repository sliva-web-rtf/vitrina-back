using System.ComponentModel.DataAnnotations;

namespace Vitrina.Domain.Project.Constructor;

public class Content
{
    /// <summary>
    /// ImageBlock id.
    /// </summary>
    [Key]
    public Guid Id { get; init; }

    /// <summary>
    /// Collection of text content blocks.
    /// </summary>
    public ICollection<NumberedBlockBase<TextBlock>> TextBlocks { get; set; }

    /// <summary>
    /// Collection of blocks with information about the team.
    /// </summary>
    public ICollection<NumberedBlockBase<CommandBlock>> CommandBlocks { get; set; }

    /// <summary>
    /// Collection of blocks with image and text.
    /// </summary>
    public ICollection<NumberedBlockBase<BlockWithTextsAndPictures>> BlocksWithTextsAndPictures { get; set; }

    /// <summary>
    /// Collection of blocks with images.
    /// </summary>
    public ICollection<NumberedBlockBase<ImageBlock>> ImageBlocks { get; set; }

    /// <summary>
    /// A collection of image carousel blocks.
    /// </summary>
    public ICollection<NumberedBlockBase<CarouselImages>> ImageCarouselBlocks { get; set; }

    /// <summary>
    /// Collection of horizontal divider blocks.
    /// </summary>
    public ICollection<NumberedBlockBase<HorizontalDivider>> HorizontalDividerBlocks { get; set; }

    /// <summary>
    /// Collection of code blocks.
    /// </summary>
    public ICollection<NumberedBlockBase<СodeBlock>> CodeBlocks { get; set; }

    /// <summary>
    /// A collection of video content blocks.
    /// </summary>
    public ICollection<NumberedBlockBase<VideoBlock>> VideoBlocks { get; set; }

    /// <summary>
    /// Project id.
    /// </summary>
    public int ProjectId { get; private set; }

    /// <summary>
    /// Project.
    /// </summary>
    required public virtual Project Project { get; set; }
}
