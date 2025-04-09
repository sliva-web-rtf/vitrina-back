namespace Vitrina.Domain.Project.Constructor;

/// <summary>
/// Carousel of images.
/// </summary>
public class CarouselImages
{
    /// <summary>
    /// ID.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Images.
    /// </summary>
    public ICollection<ImageBlock> Images { get; set; }
}
