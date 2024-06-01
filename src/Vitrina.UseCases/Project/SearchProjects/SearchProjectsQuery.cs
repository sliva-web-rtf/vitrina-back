using MediatR;
using Saritasa.Tools.Common.Pagination;
using Vitrina.UseCases.Common.Pagination;

namespace Vitrina.UseCases.Project.SearchProjects;

/// <summary>
/// Search projects.
/// </summary>
public class SearchProjectsQuery : PageQueryFilter, IRequest<PagedList<ShortProjectDto>>
{
    /// <summary>
    /// Name.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// Period.
    /// </summary>
    public string? Period { get; init; }

    /// <summary>
    /// Organization.
    /// </summary>
    public string? Organization { get; init; }

    /// <summary>
    /// Semester.
    /// </summary>
    public int? Semester { get; init; }
}
