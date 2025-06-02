using MediatR;

namespace Vitrina.UseCases.ProjectSphere.DeleteSphere;

public record DeleteSphereCommand(Guid Id) : IRequest;
