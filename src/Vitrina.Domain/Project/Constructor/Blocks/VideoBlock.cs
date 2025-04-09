namespace Vitrina.Domain.Project.Constructor;

public class VideoBlock
{
    /// <summary>
    /// ID.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Link to the video.
    /// </summary>
    public string? LinkVideo;

    /// <summary>
    /// A foreign key for the content that this block belongs to.
    /// </summary>
    public Guid ContentId;

    /// <summary>
    /// The element that the content block belongs to.
    /// </summary>
    public Content Content;
}
