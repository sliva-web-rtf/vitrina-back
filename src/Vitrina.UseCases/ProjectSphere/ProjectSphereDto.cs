using Vitrina.UseCases.ProjectPages.Blocks;

namespace Vitrina.UseCases.Project.Dto;

public record ProjectSphereDto : BaseEntityDto<Guid>
{
    required public string Name { get; init; }
}
