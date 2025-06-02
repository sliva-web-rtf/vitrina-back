using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace Vitrina.UseCases.ProjectThematics.UpdateThematics;

public record UpdateThematicsCommand(Guid Id, JsonPatchDocument<RequestThematicsDto> PatchDocument)
    : IRequest<ResponceThematicsDto>;
