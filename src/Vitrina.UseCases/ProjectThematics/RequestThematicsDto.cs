using Vitrina.UseCases.ProjectPages.Blocks;

namespace Vitrina.UseCases.ProjectThematics;

public record RequestThematicsDto : BaseEntityDto<Guid>
{
    required public string Name { get; init; }
}
