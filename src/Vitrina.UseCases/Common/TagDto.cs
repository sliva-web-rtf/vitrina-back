namespace Vitrina.UseCases.Common;

/// <summary>
/// Tag dto.
/// </summary>
public class TagDto
{
    /// <summary>
    /// Tag id.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Tag name.
    /// </summary>
    required public string Name { get; init; }
}
