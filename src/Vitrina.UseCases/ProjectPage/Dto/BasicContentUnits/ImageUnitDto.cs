using Vitrina.UseCases.Common.DTO;

namespace Vitrina.UseCases.ProjectPages.BasicContentUnits;

public record ImageUnitDto
{
    /// <summary>
    ///     A row with styles for an image.
    /// </summary>
    public FileDto? Css { get; init; }

    /// <summary>
    ///     Image url.
    /// </summary>
    required public FileDto Image { get; init; }
}
