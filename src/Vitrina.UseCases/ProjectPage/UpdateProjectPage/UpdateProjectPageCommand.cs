using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace Vitrina.UseCases.ProjectPages.UpdateProjectPage;

public record UpdateProjectPageCommand(Guid Id, JsonPatchDocument<ProjectPageDto> PatchDocument) : IRequest;
