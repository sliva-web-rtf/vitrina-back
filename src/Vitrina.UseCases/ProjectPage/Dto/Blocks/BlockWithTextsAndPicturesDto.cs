using Vitrina.UseCases.ProjectPage.Dto.Blocks;

namespace Vitrina.UseCases.ProjectPages.Blocks;

public record BlockWithTextsAndPicturesDto : NumberedBlockBaseDto
{
    /// <summary>
    ///     Collection of images with text.
    /// </summary>
    public ICollection<ImageAndTextDto> ImagesWithText { get; init; } = new List<ImageAndTextDto>();
}
