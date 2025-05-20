using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace Vitrina.UseCases.ProjectTeam.Role.UpdateRole;

/// <summary>
///     Command for updating the role.
/// </summary>
public record UpdateRoleCommand(JsonPatchDocument<RequestRoleDto> PatchDocument, int Id) : IRequest<ResponceRoleDto>;
