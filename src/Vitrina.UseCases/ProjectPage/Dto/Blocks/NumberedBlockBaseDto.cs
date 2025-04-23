namespace Vitrina.UseCases.ProjectPages.Blocks;

public abstract record NumberedBlockBaseDto : BaseEntityDto<Guid>
{
    /// <summary>
    ///     Serial number on the design canvas.
    /// </summary>
    required public int NumberOnCanvas { get; init; }
}
