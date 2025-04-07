namespace Vitrina.Domain.Project.Constructor;

/// <summary>
/// A block of content, an image with text.
/// </summary>
public class ImageWithText
{
    /// <summary>
    /// Image.
    /// </summary>
    public Content Image { get; set; }

    /// <summary>
    /// Text.
    /// </summary>
    public string Text { get; set; }
}
