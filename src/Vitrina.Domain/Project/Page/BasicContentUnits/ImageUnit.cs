using Vitrina.Domain.File;

namespace Vitrina.Domain.Project.Page.BasicContentUnits;

public class ImageUnit : BaseEntity<Guid>
{
    /// <summary>
    ///     A row with styles for an image.
    /// </summary>
    public CloudFile? Css { get; set; }

    /// <summary>
    ///     Image url.
    /// </summary>
    required public CloudFile Image { get; set; }
}
