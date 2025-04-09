namespace Vitrina.Domain.Project.Constructor;

public class BlockWithTextsAndPictures
{
    /// <summary>
    /// ID.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Collection of images with text.
    /// </summary>
    public ICollection<ImageWithText> ImagesWithText { get; set; }

    /// <summary>
    /// A foreign key for the content that this block belongs to.
    /// </summary>
    public Guid ContentId;

    /// <summary>
    /// The element that the content block belongs to.
    /// </summary>
    public Content Content;
}
