namespace Vitrina.Domain.Project.Page.Editor;

public class PageEditor : BaseEntity<Guid>
{
    required public int UserId { get; init; }

    public virtual User.User User { get; init; }

    required public Guid PageId { get; init; }

    public virtual ProjectPage Page { get; init; }

    public EditorStatus Status { get; set; }
}
