using Vitrina.Domain.Project.Page.BasicContentUnits;

namespace Vitrina.Domain.Project.Page.Blocks;

public class BlockWithTextsAndPictures : NumberedBlockBase
{
    /// <summary>
    ///     Collection of images with text.
    /// </summary>
    public ICollection<UnitWithImageAndText> ImagesWithText { get; init; } = new List<UnitWithImageAndText>();
}
