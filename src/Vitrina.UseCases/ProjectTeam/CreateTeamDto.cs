using Vitrina.UseCases.ProjectTeam.Teammate;

namespace Vitrina.UseCases.ProjectTeam;

public record CreateTeamDto
{
    public string Name { get; init; }

    public Guid ProjectId { get; init; }

    public ICollection<RequestTeammateDto> TeamMembers { get; init; } = new List<RequestTeammateDto>();
}
