using Vitrina.UseCases.ProjectPages.BasicContentUnits;

namespace Vitrina.UseCases.ProjectPages.Blocks;

public record TextBlockDto : NumberedBlockBaseDto
{
    /// <summary>
    ///     Content.
    /// </summary>
    required public TextUnitDto Text { get; init; }
}
