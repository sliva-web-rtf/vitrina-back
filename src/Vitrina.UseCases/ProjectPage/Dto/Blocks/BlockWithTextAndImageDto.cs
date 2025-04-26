using Vitrina.UseCases.ProjectPage.Dto.BasicContentUnits;

namespace Vitrina.UseCases.ProjectPage.Dto.Blocks;

/// <summary>
/// Content unit consisting of an image and text.
/// </summary>
public record BlockWithTextAndImageDto
{
    /// <summary>
    /// Image with the text.
    /// </summary>
    required public UnitWithImageAndTextDto ImageWithText { get; set; }
}
