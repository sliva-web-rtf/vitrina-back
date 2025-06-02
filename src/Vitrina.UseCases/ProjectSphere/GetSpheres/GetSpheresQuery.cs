using MediatR;

namespace Vitrina.UseCases.ProjectSphere.GetSpheres;

/// <inheritdoc />
public record GetSpheresQuery : IRequest<ICollection<ResponceSphereDto>>;
