using Vitrina.UseCases.ProjectPages.BasicContentUnits;

namespace Vitrina.UseCases.ProjectPages.Blocks;

public record ImageBlockDto : NumberedBlockBaseDto
{
    /// <summary>
    ///     Image.
    /// </summary>
    required public ImageUnitDto Image { get; init; }
}
