namespace Vitrina.Domain.Project.Page;

public class PageEditor : BaseEntity<Guid>
{
    public Guid UserId { get; set; }

    public User.User User { get; set; }

    public Guid PageId { get; set; }

    public ProjectPage Page { get; set; }

    public EditorStatus Status { get; set; }
}
