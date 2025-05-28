using Vitrina.UseCases.ProjectSphere;
using Vitrina.UseCases.ProjectThematics;

namespace Vitrina.UseCases.Project.Dto;

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
    public virtual RequestSphereDto? Sphere { get; set; }

    /// <summary>
    ///     Project type.
    /// </summary>
    public RequestThematicsDto? Thematics { get; set; }

    /// <summary>
    ///     The project supervisor's ID.
    /// </summary>
    public int? CuratorId { get; set; }

    /// <summary>
    ///     Project team id.
    /// </summary>
    public Guid? TeamId { get; init; }
}
