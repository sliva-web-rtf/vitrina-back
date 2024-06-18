using MediatR;
using Vitrina.Domain.Project;
using Vitrina.UseCases.Common;

namespace Vitrina.UseCases.Project.AddProject;

/// <summary>
/// Add project command.
/// </summary>
public class AddProjectCommand : IRequest<int>
{
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
    /// Project period.
    /// </summary>
    required public string Period { get; init; }

    /// <summary>
    /// Semester.
    /// </summary>
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
    /// Project tags.
    /// </summary>
    public ICollection<TagDto> Tags { get; init; } = new List<TagDto>();

    /// <summary>
    /// Project team.
    /// </summary>
    public ICollection<UserDto> Users { get; init; } = new List<UserDto>();
}
