using MediatR;
using Vitrina.UseCases.Common;

namespace Vitrina.UseCases.Project.UpdateProject;

/// <summary>
/// Update project command.
/// </summary>
public class UpdateProjectCommand : IRequest
{
    /// <summary>
    /// Id of project.
    /// </summary>
    public int ProjectId { get; init; }

    /// <summary>
    /// Updated project.
    /// </summary>
    required public ProjectDto Project { get; init; }
}
