using MediatR;
using Vitrina.Domain.Project.Page;
using Vitrina.UseCases.ProjectPages.Blocks;

namespace Vitrina.UseCases.ProjectPages.CreateProjectPage;

/// <inheritdoc />
public record CreateProjectPageCommand : IRequest<Guid>
{
    /// <summary>
    ///     Page status.
    /// </summary>
    public PageReadyStatusEnum ReadyStatus { get; init; }

    /// <summary>
    ///     Collection of text content blocks.
    /// </summary>
    public ICollection<TextBlockDto> TextBlocks { get; init; } = new List<TextBlockDto>();

    /// <summary>
    ///     Collection of blocks with information about the team.
    /// </summary>
    public ICollection<CommandBlockDto> CommandBlocks { get; init; } = new List<CommandBlockDto>();

    /// <summary>
    ///     Collection of blocks with image and text.
    /// </summary>
    public ICollection<BlockWithTextsAndPicturesDto> BlocksWithTextsAndPictures { get; init; } =
        new List<BlockWithTextsAndPicturesDto>();

    /// <summary>
    ///     Collection of blocks with images.
    /// </summary>
    public ICollection<ImageBlockDto> ImageBlocks { get; init; } = new List<ImageBlockDto>();

    /// <summary>
    ///     A collection of image carousel blocks.
    /// </summary>
    public ICollection<CarouselImagesDto> ImageCarouselBlocks { get; init; } = new List<CarouselImagesDto>();

    /// <summary>
    ///     Collection of horizontal divider blocks.
    /// </summary>
    public ICollection<HorizontalDividerDto> HorizontalDividerBlocks { get; init; } =
        new List<HorizontalDividerDto>();

    /// <summary>
    ///     Collection of code blocks.
    /// </summary>
    public ICollection<CodeBlockDto> CodeBlocks { get; init; } = new List<CodeBlockDto>();

    /// <summary>
    ///     A collection of video content blocks.
    /// </summary>
    public ICollection<VideoBlockDto> VideoBlocks { get; init; } = new List<VideoBlockDto>();

    /// <summary>
    ///     Project id.
    /// </summary>
    public int ProjectId { get; init; }

    /// <summary>
    ///     User ID who created the project page.
    /// </summary>
    public int CreatorId { get; init; }
}
