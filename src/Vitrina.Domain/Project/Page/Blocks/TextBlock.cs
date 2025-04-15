using Vitrina.Domain.Project.Page.BasicContentUnits;

namespace Vitrina.Domain.Project.Page.Blocks;

/// <inheritdoc />
public class TextBlock : NumberedBlockBase
{
    /// <summary>
    ///     Content.
    /// </summary>
    required public TextUnit Text { get; set; }
}
