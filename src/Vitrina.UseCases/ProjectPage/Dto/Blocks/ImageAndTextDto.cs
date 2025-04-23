using Vitrina.UseCases.ProjectPage.Dto.BasicContentUnits;
using Vitrina.UseCases.ProjectPages.Blocks;

namespace Vitrina.UseCases.ProjectPage.Dto.Blocks;

/// <summary>
/// Content unit consisting of an image and text.
/// </summary>
public record ImageAndTextDto : NumberedBlockBaseDto
{
    /// <summary>
    /// Image with the text.
    /// </summary>
    required public UnitWithImageAndTextDto ImageWithText { get; set; }
}
