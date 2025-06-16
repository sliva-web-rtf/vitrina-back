using System.ComponentModel.DataAnnotations;

namespace Vitrina.UseCases.ProjectPage.Dto.BasicContentUnits;

public abstract record UnitWithImageBaseDto
{
    /// <summary>
    ///     Image url.
    /// </summary>
    required public Guid ImageId { get; init; }

    /// <summary>
    ///     Block styles.
    /// </summary>
    [StringLength(1_000_000, ErrorMessage = "The line of styles is too big")]
    public string? Css { get; init; }
}
