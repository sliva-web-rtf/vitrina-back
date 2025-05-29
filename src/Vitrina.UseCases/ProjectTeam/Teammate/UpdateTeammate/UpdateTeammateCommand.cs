using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace Vitrina.UseCases.ProjectTeam.Teammate.UpdateTeammate;

/// <summary>
///     Command for updating the information of a team member.
/// </summary>
public record UpdateTeammateCommand(JsonPatchDocument<ResponceTeammateDto> PatchDocument, int Id, int IdAuthorizedUser)
    : IRequest<ResponceTeammateDto>;
