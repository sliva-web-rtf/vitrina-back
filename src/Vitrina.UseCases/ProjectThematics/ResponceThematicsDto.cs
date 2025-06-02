using Vitrina.UseCases.Common.DTO;

namespace Vitrina.UseCases.ProjectThematics;

public record ResponceThematicsDto : BaseEntityDto<Guid>
{
    required public string Name { get; init; }
}
