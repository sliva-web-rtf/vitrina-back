using Vitrina.Domain.Project.Page.BasicContentUnits;

namespace Vitrina.Domain.Project.Page.Blocks;

/// <inheritdoc />
public class ImageBlock : NumberedBlockBase
{
    /// <summary>
    ///     Image.
    /// </summary>
    required public ImageUnit Image { get; set; }
}
