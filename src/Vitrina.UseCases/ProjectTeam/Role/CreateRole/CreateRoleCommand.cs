using MediatR;

namespace Vitrina.UseCases.ProjectTeam.Role.CreateRole;

/// <summary>
///     Command to create a role.
/// </summary>
public record CreateRoleCommand(RequestRoleDto RoleDto) : IRequest<int>;
