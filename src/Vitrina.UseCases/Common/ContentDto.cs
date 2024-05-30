namespace Vitrina.UseCases.Common;

/// <summary>
/// Content Dto.
/// </summary>
public class ContentDto
{
    /// <summary>
    /// Bytes of image.
    /// </summary>
    public ICollection<byte> ImageBytes { get; set; } = new List<byte>();
}
