using MediatR;

namespace Vitrina.UseCases.ProjectTeam.Role.DeleteRole;

/// <summary>
///     Command to remove the role.
/// </summary>
public record DeleteRoleCommand(int Id) : IRequest;
