namespace Vitrina.Domain.Project.Constructor;

/// <summary>
/// A text block of content.
/// </summary>
public class TextBlock
{
    /// <summary>
    /// ID.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Link to an html document in the cloud.
    /// </summary>
    public string? LinkToHtmlFile { get; set; }

    /// <summary>
    /// Link to an css document in the cloud.
    /// </summary>
    public string? LinkToCssFile { get; set; }

    /// <summary>
    /// A foreign key for the content that this block belongs to.
    /// </summary>
    public Guid ContentId;

    /// <summary>
    /// The element that the content block belongs to.
    /// </summary>
    public Content Content;
}
