using Vitrina.UseCases.ProjectTeam.Teammate;

namespace Vitrina.UseCases.ProjectTeam;

public record CreateTeamDto
{
    public string Name { get; init; }

    public int ProjectId { get; init; }

    public ICollection<RequestTeammateDto> TeamMembers { get; init; } = new List<RequestTeammateDto>();
}
