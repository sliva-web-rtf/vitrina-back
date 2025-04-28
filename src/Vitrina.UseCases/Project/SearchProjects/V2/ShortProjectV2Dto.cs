using System.Text.Json.Serialization;
using Vitrina.Domain.Project;
using Vitrina.UseCases.Common.DTO;

namespace Vitrina.UseCases.Project.SearchProjects.V2;

/// <summary>
///     Short project dto.
/// </summary>
public class ShortProjectV2Dto
{
    /// <summary>
    ///     Project id.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    ///     Name.
    /// </summary>
    required public string Name { get; init; }

    /// <summary>
    ///     Project description.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    ///     Period.
    /// </summary>
    [JsonIgnore]
    public string? Period { get; init; }

    /// <summary>
    ///     Image url.
    /// </summary>
    public string ImageUrl { get; set; } = "";

    /// <summary>
    ///     Priority of project.
    /// </summary>
    public int Priority { get; set; }

    /// <summary>
    ///     Path to preview image.
    /// </summary>
    public string? PreviewImagePath { get; set; }

    /// <summary>
    ///     Organization.
    /// </summary>
    [JsonIgnore]
    public string? Client { get; init; }

    /// <summary>
    ///     Semester.
    /// </summary>
    [JsonIgnore]
    public SemesterEnum Semester { get; init; }

    /// <summary>
    ///     Tags.
    /// </summary>
    public ICollection<TagDto> Tags { get; init; } = new List<TagDto>();
}
