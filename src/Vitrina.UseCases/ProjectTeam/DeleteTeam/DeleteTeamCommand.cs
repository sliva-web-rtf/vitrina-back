using MediatR;

namespace Vitrina.UseCases.ProjectTeam.DeleteTeam;

/// <summary>
///     The command to delete the specified team.
/// </summary>
public record DeleteTeamCommand(Guid Id, int IdAuthorizedUser) : IRequest;
