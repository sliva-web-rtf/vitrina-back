using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Vitrina.UseCases.Project.Dto;

namespace Vitrina.UseCases.Project.UpdateProject;

/// <summary>
///     Update project command.
/// </summary>
public record UpdateProjectCommand(
    Guid ProjectId,
    JsonPatchDocument<UpdateProjectDto> PatchDocument,
    int IdAuthorizedUser)
    : IRequest<ResponceProjectDto>;
