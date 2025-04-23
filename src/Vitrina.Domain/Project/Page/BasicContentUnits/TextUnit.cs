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
    required public File.File Html { get; set; }

    /// <summary>
    ///     Link to an css document in the cloud.
    /// </summary>
    public File.File? Css { get; set; }
}
