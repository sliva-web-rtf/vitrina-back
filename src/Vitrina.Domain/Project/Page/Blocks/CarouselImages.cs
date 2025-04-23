using Vitrina.Domain.Project.Page.BasicContentUnits;

namespace Vitrina.Domain.Project.Page.Blocks;

/// <summary>
///     Carousel of images.
/// </summary>
public class CarouselImages : NumberedBlockBase
{
    /// <summary>
    ///     Images.
    /// </summary>
    public ICollection<ImageUnit> Images { get; init; } = new List<ImageUnit>();
}
