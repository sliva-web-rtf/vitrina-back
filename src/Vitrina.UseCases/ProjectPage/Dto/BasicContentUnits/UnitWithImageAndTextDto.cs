using System.ComponentModel.DataAnnotations;
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
    [StringLength(1_000_000, ErrorMessage = "The line of styles is too big")]
    public string? Css { get; init; }

    /// <summary>
    ///     Image url.
    /// </summary>
    required public FileDto Image { get; init; }

    /// <summary>
    ///     HTML block marking.
    /// </summary>
    [StringLength(1_000_000, ErrorMessage = "The markup is too voluminous")]
    required public string Html { get; init; }
}
