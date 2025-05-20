using MediatR;

namespace Vitrina.UseCases.ProjectTeam.Role.GetRoles;

/// <summary>
///     Query to get roles.
/// </summary>
public record GetRolesQuery : IRequest<ICollection<ResponceRoleDto>>;
