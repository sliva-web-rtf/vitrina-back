using MediatR;

namespace Vitrina.UseCases.ProjectTeam.Teammate.GetTeammateById;

/// <summary>
///     Query to receive a member of the ID team.
/// </summary>
public record GetTeammateByIdQuery(int Id) : IRequest<ResponceTeammateDto>;
