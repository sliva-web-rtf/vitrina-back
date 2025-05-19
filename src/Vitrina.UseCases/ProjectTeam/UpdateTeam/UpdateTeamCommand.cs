using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace Vitrina.UseCases.ProjectTeam.UpdateTeam;

/// <summary>
///     Command to update the team by id.
/// </summary>
public record UpdateTeamCommand(Guid Id, JsonPatchDocument<UpdateTeamDto> PatchDocument, int IdAuthorizedUser)
    : IRequest<ResponceTeamDto>;
