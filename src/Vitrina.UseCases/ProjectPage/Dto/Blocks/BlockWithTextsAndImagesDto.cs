namespace Vitrina.UseCases.ProjectPage.Dto.Blocks;

public record BlockWithTextsAndImagesDto
{
    /// <summary>
    ///     Collection of images with text.
    /// </summary>
    public ICollection<BlockWithTextAndImageDto> ImagesWithText { get; init; } = new List<BlockWithTextAndImageDto>();
}
