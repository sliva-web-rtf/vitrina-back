namespace Vitrina.UseCases.Common.DTO;

public class BlockDto
{
    /// <summary>
    ///     Title of block.
    /// </summary>
    required public string Title { get; set; }

    /// <summary>
    ///     Text of block.
    /// </summary>
    required public string Text { get; set; }
}
