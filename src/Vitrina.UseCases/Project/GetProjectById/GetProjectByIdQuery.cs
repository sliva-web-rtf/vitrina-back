using MediatR;
using Vitrina.UseCases.Common;

namespace Vitrina.UseCases.Project.GetProjectById;

/// <summary>
/// Get project query.
/// </summary>
public record GetProjectByIdQuery : IRequest<ProjectDto>
{
    /// <summary>
    /// Project id.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetProjectByIdQuery(int id)
    {
        Id = id;
    }
}
