namespace Vitrina.Domain.Project.Page.Editor;

public class PageEditor : BaseEntity<Guid>
{
    public int UserId { get; set; }

    public User.User User { get; set; }

    public Guid PageId { get; set; }

    public ProjectPage Page { get; set; }

    public EditorStatus Status { get; set; }
}
