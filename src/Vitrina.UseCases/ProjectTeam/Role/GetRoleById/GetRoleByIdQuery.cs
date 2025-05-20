using MediatR;

namespace Vitrina.UseCases.ProjectTeam.Role.GetRoleById;

/// <summary>
///     Query to create a role.
/// </summary>
public record GetRoleByIdQuery(int Id) : IRequest<ResponceRoleDto>;
