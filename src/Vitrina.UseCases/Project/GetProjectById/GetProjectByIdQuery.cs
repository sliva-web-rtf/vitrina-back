using MediatR;
using Vitrina.UseCases.Common;
using Vitrina.UseCases.Common.DTO;

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
