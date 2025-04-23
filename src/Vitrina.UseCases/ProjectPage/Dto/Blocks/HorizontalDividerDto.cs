namespace Vitrina.UseCases.ProjectPages.Blocks;

public record HorizontalDividerDto : NumberedBlockBaseDto
{
    /// <summary>
    ///     Size in pixels.
    /// </summary>
    required public int SizeInPixels { get; init; }
}
