namespace Vitrina.Domain;

public class ProjectThematics : BaseEntity<Guid>
{
    required public string Name { get; init; }
}
