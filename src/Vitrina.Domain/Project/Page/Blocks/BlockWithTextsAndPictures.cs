namespace Vitrina.Domain.Project.Page.Blocks;

public class BlockWithTextsAndPictures : NumberedBlockBase
{
    /// <summary>
    ///     Collection of images with text.
    /// </summary>
    public ICollection<ImageWithText> ImagesWithText { get; set; }
}
