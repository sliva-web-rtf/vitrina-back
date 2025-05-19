using MediatR;

namespace Vitrina.UseCases.ProjectTeam.CreateTeam;

/// <summary>
///     Command to create a team.
/// </summary>
public record CreateTeamCommand(CreateTeamDto TeamDto, int IdAuthorizedUser) : IRequest<Guid>;
