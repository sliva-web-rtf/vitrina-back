using Vitrina.UseCases.ProjectPage.Dto.BasicContentUnits;

namespace Vitrina.UseCases.ProjectPage.Dto.Blocks;

public record CarouselImagesDto
{
    /// <summary>
    ///     Images.
    /// </summary>
    required public ICollection<ImageUnitDto> Images { get; init; } = new List<ImageUnitDto>();
}
