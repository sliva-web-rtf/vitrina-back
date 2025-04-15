using Vitrina.Domain.Project.Page.BasicContentUnits;

namespace Vitrina.Domain.Project.Page.Blocks;

/// <inheritdoc />
public class ImageBlock : NumberedBlockBase
{
    /// <summary>
    ///     Content.
    /// </summary>
    required public ImageUnit Image { get; set; }
}
