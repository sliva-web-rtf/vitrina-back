using MediatR;
using Saritasa.Tools.Common.Pagination;
using Vitrina.UseCases.Common.Pagination;
using Vitrina.UseCases.Project.Dto;

namespace Vitrina.UseCases.Project.GetProjects;

/// <summary>
///     Search projects.
/// </summary>
public record GetProjectsQuery : PageQueryFilter, IRequest<PagedList<ProjectDto>>
{
    /// <summary>
    ///     Project name.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    ///     Project customer.
    /// </summary>
    public string? Client { get; init; }

    /// <summary>
    ///     Project type.
    /// </summary>
    public string? Thematics { get; init; }

    /// <summary>
    ///     Sphere.
    /// </summary>
    public string? Sphere { get; init; }
}
