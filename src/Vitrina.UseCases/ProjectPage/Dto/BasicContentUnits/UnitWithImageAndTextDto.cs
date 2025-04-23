using Vitrina.UseCases.Common.DTO;

namespace Vitrina.UseCases.ProjectPage.Dto.BasicContentUnits;

/// <summary>
/// Content unit consisting of an image and text.
/// </summary>
public class UnitWithImageAndTextDto
{
    /// <summary>
    ///     A row with styles for an image.
    /// </summary>
    public FileDto? Css { get; init; }

    /// <summary>
    ///     Image url.
    /// </summary>
    required public FileDto Image { get; init; }

    /// <summary>
    ///     Link to an html document in the cloud.
    /// </summary>
    required public FileDto Html { get; init; }
}
