using MediatR;

namespace Vitrina.UseCases.ProjectSphere.GetSphereById;

public record GetSphereByIdQuery(Guid Id) : IRequest<ResponceSphereDto>;
