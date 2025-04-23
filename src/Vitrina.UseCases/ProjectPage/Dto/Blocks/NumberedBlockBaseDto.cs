using System.ComponentModel.DataAnnotations;

namespace Vitrina.UseCases.ProjectPages.Blocks;

public abstract record NumberedBlockBaseDto : BaseEntityDto<Guid>
{
    /// <summary>
    ///     Serial number on the design canvas.
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = $"Value for {nameof(NumberOnCanvas)} must be positive.")]
    required public int NumberOnCanvas { get; init; }
}
