namespace Vitrina.UseCases.Common;

/// <summary>
/// Project dto.
/// </summary>
public class ProjectDto
{
    /// <summary>
    /// Project id.
    /// </summary>
    required public int Id { get; init; }

    /// <summary>
    /// Name.
    /// </summary>
    required public string Name { get; init; }

    /// <summary>
    /// Project description.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Project aim.
    /// </summary>
    public string? Aim { get; init; }

    /// <summary>
    /// Project client.
    /// </summary>
    public string? Client { get; init; }

    /// <summary>
    /// Markdown of page.
    /// </summary>
    public string? Markdown { get; init; }

    /// <summary>
    /// Url of project video.
    /// </summary>
    public string? VideoUrl { get; init; }

    /// <summary>
    /// Project content.
    /// </summary>
    public ICollection<ContentDto> Contents { get; init; } = new List<ContentDto>();

    /// <summary>
    /// Project tags.
    /// </summary>
    public ICollection<TagDto> Tags { get; init; } = new List<TagDto>();

    /// <summary>
    /// Project team.
    /// </summary>
    public ICollection<UserDto> Users { get; init; } = new List<UserDto>();
}
