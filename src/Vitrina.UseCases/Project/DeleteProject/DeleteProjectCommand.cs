using MediatR;

namespace Vitrina.UseCases.Project.DeleteProject;

/// <summary>
///     Delete project command.
/// </summary>
public record DeleteProjectCommand(Guid ProjectId, int IdAuthorizedUser) : IRequest;
