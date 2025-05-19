using MediatR;

namespace Vitrina.UseCases.Project.Teammate.GetTeammateById;

/// <summary>
///     Query to receive a member of the ID team.
/// </summary>
public record GetTeammateByIdQuery(int Id) : IRequest<TeammateDto>;
