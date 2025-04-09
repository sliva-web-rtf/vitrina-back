using System.ComponentModel.DataAnnotations;

namespace Vitrina.Domain.Project.Constructor;

public class ImageBlock
{
    /// <summary>
    /// ImageBlock id.
    /// </summary>
    [Key]
    public Guid Id { get; init; }

    /// <summary>
    /// A row with styles for an image.
    /// </summary>
    public string? Css { get; set; }

    /// <summary>
    /// Image url.
    /// </summary>
    required public string ImageUrl { get; set; }
}
