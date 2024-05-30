using System.Text.Json.Serialization;
using Vitrina.Domain.Project;
using Vitrina.UseCases.Common;

namespace Vitrina.UseCases.Project.SearchProjects;

/// <summary>
/// Short project dto.
/// </summary>
public class ShortProjectDto
{
    /// <summary>
    /// Project id.
    /// </summary>
    [JsonIgnore]
    public int Id { get; init; }

    /// <summary>
    /// Name.
    /// </summary>
    required public string Name { get; init; }

    /// <summary>
    /// Project description.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Period.
    /// </summary>
    [JsonIgnore]
    public string? Period { get; init; }

    /// <summary>
    /// Organization.
    /// </summary>
    [JsonIgnore]
    public string? Client { get; init; }

    /// <summary>
    /// Semester.
    /// </summary>
    [JsonIgnore]
    public SemesterEnum Semester { get; init; }

    /// <summary>
    /// Tags.
    /// </summary>
    public ICollection<TagDto> Tags { get; init; } = new List<TagDto>();
}
