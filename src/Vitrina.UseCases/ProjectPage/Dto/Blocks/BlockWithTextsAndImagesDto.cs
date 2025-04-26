using Vitrina.UseCases.ProjectPage.Dto.Blocks;

namespace Vitrina.UseCases.ProjectPages.Blocks;

public record BlockWithTextsAndImagesDto
{
    /// <summary>
    ///     Collection of images with text.
    /// </summary>
    public ICollection<BlockWithTextAndImageDto> ImagesWithText { get; init; } = new List<BlockWithTextAndImageDto>();
}
