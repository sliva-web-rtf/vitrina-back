using System.ComponentModel.DataAnnotations;
using MediatR;
using Vitrina.Domain.Project;
using Vitrina.UseCases.Common;
using Vitrina.UseCases.Common.DTO;

namespace Vitrina.UseCases.Project.AddProject;

/// <summary>
/// Add project command.
/// </summary>
public class AddProjectCommand : IRequest<int>
{
    /// <summary>
    /// Name.
    /// </summary>
    [Required]
    required public string Name { get; init; }

    /// <summary>
    /// Project description.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Project aim.
    /// </summary>
    [Required]
    required public string Aim { get; init; }

    /// <summary>
    /// Project client.
    /// </summary>
    public string? Client { get; init; }

    /// <summary>
    /// Project period.
    /// </summary>
    public string Period { get; init; }

    /// <summary>
    /// Priority of project.
    /// </summary>
    [Required]
    public int Priority { get; set; }

    /// <summary>
    /// Path to preview image.
    /// </summary>
    public string? PreviewImagePath { get; set; }

    /// <summary>
    /// Semester.
    /// </summary>
    [Required]
    public SemesterEnum Semester { get; init; }

    /// <summary>
    /// Markdown of page.
    /// </summary>
    public string? Markdown { get; init; }

    /// <summary>
    /// Url of project video.
    /// </summary>
    public string? VideoUrl { get; init; }

    /// <summary>
    /// Problem of project.
    /// </summary>
    public string Problem { get; set; }

    /// <summary>
    /// Idea of project.
    /// </summary>
    public string Idea { get; set; }

    /// <summary>
    /// Problem of project.
    /// </summary>
    public string Solution { get; set; }

    public string? Sphere { get; set; }

    public string? Type { get; set; }

    /// <summary>
    /// Project tags.
    /// </summary>
    public ICollection<TagDto> Tags { get; init; } = new List<TagDto>();

    /// <summary>
    /// Project team.
    /// </summary>
    [Required]
    public ICollection<UserDto> Users { get; init; } = new List<UserDto>();

    /// <summary>
    /// Custom blocks.
    /// </summary>
    public ICollection<BlockDto> CustomBlocks { get; init; }
}
