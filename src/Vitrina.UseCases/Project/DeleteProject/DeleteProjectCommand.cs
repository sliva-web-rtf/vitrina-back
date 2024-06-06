using MediatR;

namespace Vitrina.UseCases.Project.DeleteProject;

/// <summary>
/// Delete project command.
/// </summary>
public class DeleteProjectCommand : IRequest
{
    /// <summary>
    /// Project id.
    /// </summary>
    public int ProjectId { get; init; }
}
