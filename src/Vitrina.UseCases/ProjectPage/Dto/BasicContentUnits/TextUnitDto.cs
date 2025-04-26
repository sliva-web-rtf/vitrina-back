using System.ComponentModel.DataAnnotations;

namespace Vitrina.UseCases.ProjectPages.BasicContentUnits;

public record TextUnitDto
{
    /// <summary>
    ///     Block styles.
    /// </summary>
    [StringLength(1_000_000, ErrorMessage = "The line of styles is too big")]
    public string? Css { get; init; }

    /// <summary>
    ///     HTML block marking.
    /// </summary>
    [StringLength(1_000_000, ErrorMessage = "The markup is too voluminous")]
    required public string Html { get; init; }
}
