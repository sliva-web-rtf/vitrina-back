using Vitrina.UseCases.ProjectPages.BasicContentUnits;

namespace Vitrina.UseCases.ProjectPages.Blocks;

public record CarouselImagesDto
{
    /// <summary>
    ///     Images.
    /// </summary>
    required public ICollection<ImageUnitDto> Images { get; init; } = new List<ImageUnitDto>();
}
