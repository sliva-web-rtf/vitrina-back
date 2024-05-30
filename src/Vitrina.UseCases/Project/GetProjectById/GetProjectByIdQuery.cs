using MediatR;
using Vitrina.UseCases.Common;

namespace Vitrina.UseCases.Project.GetProjectById;

/// <summary>
/// Get project query.
/// </summary>
public record GetProjectByIdQuery(int Id) : IRequest<ProjectDto>;
