using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Vitrina.UseCases.User.DTO.Profile;

namespace Vitrina.UseCases.User.UpdateUser.UpdateCurator;

/// <summary>
/// The command to update curator profile data.
/// </summary>
public record UpdateCuratorCommand(int CuratorId, JsonPatchDocument<CuratorDto> PatchDocument) : IRequest<CuratorDto>;
