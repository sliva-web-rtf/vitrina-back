namespace Vitrina.Domain.Project;

public class Team : BaseEntity<Guid>
{
    required public string Name { get; set; }

    required public Guid ProjectId { get; set; }

    public virtual Project Project { get; set; }

    public ICollection<Teammate.Teammate> TeamMembers { get; init; } = new List<Teammate.Teammate>();
}
