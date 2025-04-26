using System.ComponentModel.DataAnnotations;

namespace Vitrina.UseCases.ProjectPages.Blocks;

public record HorizontalDividerDto
{
    /// <summary>
    ///     Size in pixels.
    /// </summary>
    [Range(0, int.MaxValue, ErrorMessage = $"Value for {nameof(SizeInPixels)} must be non-negative.")]
    required public int SizeInPixels { get; init; }
}
