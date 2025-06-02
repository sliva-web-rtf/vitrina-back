using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace Vitrina.UseCases.ProjectSphere.UpdateSphere;

public record UpdateSphereCommand(Guid Id, JsonPatchDocument<RequestSphereDto> PatchDocument)
    : IRequest<ResponceSphereDto>;
