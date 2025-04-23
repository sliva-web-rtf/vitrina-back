using Vitrina.UseCases.Common.DTO;

namespace Vitrina.UseCases.ProjectPages.BasicContentUnits;

public record TextUnitDto
{
    /// <summary>
    ///     Link to an html document in the cloud.
    /// </summary>
    required public FileDto Html { get; init; }

    /// <summary>
    ///     Link to an css document in the cloud.
    /// </summary>
    public FileDto? Css { get; init; }
}
