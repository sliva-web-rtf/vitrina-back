using MediatR;
using Saritasa.Tools.Common.Pagination;
using Vitrina.UseCases.Common.Pagination;

namespace Vitrina.UseCases.Project.SearchProjects.V2;

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
    public string? Client { get; init; }

    /// <summary>
    /// Organization.
    /// </summary>
    public string? Type { get; init; }

    /// <summary>
    /// Semester.
    /// </summary>
    public string? Sphere { get; init; }
}
