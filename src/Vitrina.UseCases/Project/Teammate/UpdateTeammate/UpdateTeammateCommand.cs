using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace Vitrina.UseCases.Project.Teammate.UpdateTeammate;

/// <summary>
///     Command for updating the information of a team member.
/// </summary>
public record UpdateTeammateCommand(JsonPatchDocument<TeammateDto> PatchDocument, int Id, int IdAuthorizedUser)
    : IRequest<TeammateDto>;
