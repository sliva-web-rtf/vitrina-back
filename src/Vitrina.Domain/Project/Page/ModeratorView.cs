namespace Vitrina.Domain.Project.Page;

public class ModeratorView
{
    public Guid Id { get; init; }

    public int AdministratorId { get; init; }

    public virtual User.User Administrator { get; init; }

    public Guid PageId { get; init; }

    public virtual ProjectPage Page { get; init; }

    public DateTime Date { get; init; }
}
