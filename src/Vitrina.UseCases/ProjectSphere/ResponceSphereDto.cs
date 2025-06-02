using Vitrina.UseCases.Common.DTO;

namespace Vitrina.UseCases.ProjectSphere;

public record ResponceSphereDto : BaseEntityDto<Guid>
{
    required public string Name { get; init; }
}
