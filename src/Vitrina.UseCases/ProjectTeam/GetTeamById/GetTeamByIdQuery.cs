using MediatR;

namespace Vitrina.UseCases.ProjectTeam.GetTeamById;

/// <summary>
///     Query of getting a team by id.
/// </summary>
public record GetTeamByIdQuery(Guid Id) : IRequest<ResponceTeamDto>;
