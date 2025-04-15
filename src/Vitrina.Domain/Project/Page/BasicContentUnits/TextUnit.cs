using Vitrina.Domain.File;

namespace Vitrina.Domain.Project.Page.BasicContentUnits;

/// <summary>
///     A text block of content.
/// </summary>
public class TextUnit : BaseEntity<Guid>
{
    /// <summary>
    ///     Link to an html document in the cloud.
    /// </summary>
    required public CloudFile Html { get; set; }

    /// <summary>
    ///     Link to an css document in the cloud.
    /// </summary>
    public CloudFile? Css { get; set; }
}
