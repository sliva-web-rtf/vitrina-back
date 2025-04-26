using System.ComponentModel.DataAnnotations;

namespace Vitrina.Domain.Project.Page.BasicContentUnits;

/// <summary>
///     A text block of content.
/// </summary>
public class TextUnit : BaseEntity<Guid>
{
    /// <summary>
    ///     HTML block marking.
    /// </summary>
    required public string Html { get; set; }

    /// <summary>
    ///     Block styles.
    /// </summary>
    public string? Css { get; set; }
}
