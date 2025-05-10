using System.ComponentModel.DataAnnotations;
using Vitrina.Domain.Project;
using Vitrina.UseCases.Common.DTO;

namespace Vitrina.UseCases.Project.UpdateProject.DTO;

/// <summary>
///     Dto for update.
/// </summary>
public class UpdateProjectDto
{
    /// <summary>
    ///     Name.
    /// </summary>
    [Required]
    required public string Name { get; init; }

    /// <summary>
    ///     Project description.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    ///     Project aim.
    /// </summary>
    public string? Aim { get; init; }

    /// <summary>
    ///     Project client.
    /// </summary>
    public string? Client { get; init; }

    /// <summary>
    ///     Markdown of page.
    /// </summary>
    public string? Markdown { get; init; }

    /// <summary>
    ///     Url of project video.
    /// </summary>
    public string? VideoUrl { get; init; }

    /// <summary>
    ///     Priority of project.
    /// </summary>
    [Required]
    public int Priority { get; set; }

    /// <summary>
    ///     Semester.
    /// </summary>
    [Required]
    public SemesterEnum Semester { get; set; }

    /// <summary>
    ///     Project content.
    /// </summary>
    public ICollection<ContentDto> Contents { get; init; } = new List<ContentDto>();

    /// <summary>
    ///     Project tags.
    /// </summary>
    public ICollection<TagDto> Tags { get; init; } = new List<TagDto>();

    /// <summary>
    ///     Project team.
    /// </summary>
    public ICollection<UpdateUserDto> Users { get; init; } = new List<UpdateUserDto>();
}
