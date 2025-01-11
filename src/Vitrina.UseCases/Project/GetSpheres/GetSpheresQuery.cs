using MediatR;

namespace Vitrina.UseCases.Project.GetSpheres;

/// <summary>
/// Get projects spheres.
/// </summary>
public class GetSpheresQuery : IRequest<ICollection<string>>
{
}
