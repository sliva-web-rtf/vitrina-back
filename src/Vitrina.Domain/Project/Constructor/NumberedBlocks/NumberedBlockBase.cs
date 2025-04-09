using System.ComponentModel.DataAnnotations;

namespace Vitrina.Domain.Project.Constructor;

/// <summary>
/// Numbered content unit.
/// </summary>
/// <typeparam name="TBlock">Type of content block</typeparam>
public class NumberedBlockBase<TBlock>
{
    /// <summary>
    /// ID.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Serial number on the design canvas.
    /// </summary>
    public int NumberOnCanvas { get; set; }

    /// <summary>
    /// The content block.
    /// </summary>
    public TBlock Block { get; set; }
}
