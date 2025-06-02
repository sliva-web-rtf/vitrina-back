using MediatR;

namespace Vitrina.UseCases.ProjectSphere.CreateSphere;

public record CreateSphereCommand(RequestSphereDto SphereDto) : IRequest<Guid>;
