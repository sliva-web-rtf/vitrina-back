using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Vitrina.UseCases.ProjectPage.Dto;

namespace Vitrina.UseCases.ProjectPage.UpdateProjectPage;

/// <inheritdoc />
public record UpdateProjectPageCommand(Guid Id, JsonPatchDocument<ProjectPageDto> PatchDocument) : IRequest;
