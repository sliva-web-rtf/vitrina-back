using System.ComponentModel.DataAnnotations;

namespace Vitrina.Domain.Project.Constructor;

/// <summary>
/// A block of content consisting of an image and text.
/// </summary>
public class ImageWithText
{
    /// <summary>
    /// ID.
    /// </summary>
    [Key]
    public Guid Id { get; init; }

    /// <summary>
    /// The position of the image in the content block.
    /// </summary>
    public ImagePositionEnum ImagePosition { get; set; }

    /// <summary>
    /// Image.
    /// </summary>
    public ImageBlock Image { get; set; }

    /// <summary>
    /// Text in html format.
    /// </summary>
    public TextBlock Text { get; set; }
}
