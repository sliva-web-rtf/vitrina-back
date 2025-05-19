using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Vitrina.UseCases.ProjectTeam.Teammate;

namespace Vitrina.UseCases.Project.Teammate.UpdateTeammate;

/// <summary>
///     Command for updating the information of a team member.
/// </summary>
public record UpdateTeammateCommand(JsonPatchDocument<ResponceTeammateDto> PatchDocument, int Id, int IdAuthorizedUser)
    : IRequest<ResponceTeammateDto>;
