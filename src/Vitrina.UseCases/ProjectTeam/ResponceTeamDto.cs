using Vitrina.UseCases.Common.DTO;

namespace Vitrina.UseCases.ProjectTeam;

public record ResponceTeamDto : BaseEntityDto<Guid>
{
    public string Name { get; init; }

    public int ProjectId { get; init; }
}
