namespace Vitrina.Domain;

/// <summary>
///     Image.
/// </summary>
public class Image
{
    required public Guid Id { get; init; }

    required public Guid FileId { get; init; }

    public File File { get; init; }
}
