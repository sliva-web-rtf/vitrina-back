using Vitrina.Domain.Project.Page.BasicContentUnits;

namespace Vitrina.Domain.Project.Page.Blocks;

/// <summary>
///     A block of content consisting of an image and text.
/// </summary>
public class ImageWithText : NumberedBlockBase
{
    /// <summary>
    ///     The position of the image in the content block.
    /// </summary>
    public ImagePositionEnum ImagePosition { get; set; }

    /// <summary>
    ///     Image.
    /// </summary>
    required public ImageUnit Image { get; set; }

    /// <summary>
    ///     Text in html format.
    /// </summary>
    required public TextUnit Text { get; set; }
}
