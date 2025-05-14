namespace Vitrina.UseCases.Common.DTO;

/// <summary>
///     Project dto.
/// </summary>
public record ProjectDto
{
    /// <summary>
    /// Project name.
    /// </summary>
    required public string Name { get; init; }

    /// <summary>
    ///     Project description.
    /// </summary>
    required public string Description { get; init; }

    /// <summary>
    ///     Path to preview image.
    /// </summary>
    required public string PreviewImagePath { get; set; }

    /// <summary>
    /// Project client.
    /// </summary>
    public string? Client { get; init; }

    /// <summary>
    ///     Project page id.
    /// </summary>
    required public Guid PageId { get; init; }

    /// <summary>
    ///     Project sphere.
    /// </summary>
    public string? Sphere { get; init; }

    /// <summary>
    ///     Project type.
    /// </summary>
    public string? Type { get; init; }
}
