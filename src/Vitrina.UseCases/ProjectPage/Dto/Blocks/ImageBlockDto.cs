using Vitrina.UseCases.ProjectPage.Dto.BasicContentUnits;

namespace Vitrina.UseCases.ProjectPage.Dto.Blocks;

public record ImageBlockDto
{
    /// <summary>
    ///     Image.
    /// </summary>
    required public ImageUnitDto Image { get; init; }
}
