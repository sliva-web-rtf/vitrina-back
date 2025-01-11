using MediatR;
using Saritasa.Tools.Common.Pagination;
using Vitrina.UseCases.Common.Pagination;

namespace Vitrina.UseCases.Project.SearchProjects.V2;

/// <summary>
/// Search projects.
/// </summary>
public class SearchProjectsV2Query : PageQueryFilter, IRequest<PagedList<ShortProjectV2Dto>>
{
    /// <summary>
    /// Project name.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// Project customer.
    /// </summary>
    public string? Customer { get; init; }

    /// <summary>
    /// Project type.
    /// </summary>
    public string? ProjectType { get; init; }

    /// <summary>
    /// Sphere.
    /// </summary>
    public string? Sphere { get; init; }
}
