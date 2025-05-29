using Vitrina.UseCases.Common.DTO;

namespace Vitrina.UseCases.ProjectSphere;

public record RequestSphereDto : BaseEntityDto<Guid>
{
    required public string Name { get; init; }
}
