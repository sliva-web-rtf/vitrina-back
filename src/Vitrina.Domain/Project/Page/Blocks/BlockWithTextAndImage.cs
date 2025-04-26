using Vitrina.Domain.Project.Page.BasicContentUnits;

namespace Vitrina.Domain.Project.Page.Blocks;

/// <summary>
///     A block of content consisting of an image and text.
/// </summary>
public class BlockWithTextAndImage : NumberedBlockBase
{
    /// <summary>
    ///     Image with the text.
    /// </summary>
    required public UnitWithImageAndText ImageWithText { get; set; }
}
