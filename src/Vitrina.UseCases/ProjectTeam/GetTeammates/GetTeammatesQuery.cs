using MediatR;
using Vitrina.UseCases.ProjectTeam.Teammate;

namespace Vitrina.UseCases.ProjectTeam.GetTeammates;

/// <inheritdoc />
public record GetTeammatesQuery(Guid Id) : IRequest<ICollection<ResponceTeammateDto>>;
