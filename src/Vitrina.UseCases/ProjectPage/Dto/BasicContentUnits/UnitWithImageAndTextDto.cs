using System.ComponentModel.DataAnnotations;

namespace Vitrina.UseCases.ProjectPage.Dto.BasicContentUnits;

/// <summary>
///     Content unit consisting of an image and text.
/// </summary>
public record UnitWithImageAndTextDto : UnitWithImageBaseDto
{
    /// <summary>
    ///     The marking of the text.
    /// </summary>
    [StringLength(1_000_000, ErrorMessage = "The markup is too voluminous")]
    required public string Markdown { get; init; }
}
