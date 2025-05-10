using MediatR;
using Vitrina.UseCases.Project.UpdateProject.DTO;

namespace Vitrina.UseCases.Project.UpdateProject;

/// <summary>
///     Update project command.
/// </summary>
public class UpdateProjectCommand : IRequest
{
    /// <summary>
    ///     Id of project.
    /// </summary>
    public int ProjectId { get; init; }

    /// <summary>
    ///     Updated project.
    /// </summary>
    required public UpdateProjectDto Project { get; init; }
}
