using Vitrina.Domain.Project.Page;

namespace Vitrina.Domain.Project;

/// <summary>
///     Domain class of project.
/// </summary>
public class Project : BaseEntity<int>
{
    /// <summary>
    ///     Project name.
    /// </summary>
    required public string Name { get; set; }

    /// <summary>
    ///     Project aim.
    /// </summary>
    public string? Aim { get; set; }

    /// <summary>
    ///     Project client.
    /// </summary>
    public string? Client { get; set; }

    /// <summary>
    ///     Semester.
    /// </summary>
    public SemesterEnum Semester { get; set; }

    /// <summary>
    ///     Path to preview image.
    /// </summary>
    public string? PreviewImagePath { get; set; }

    /// <summary>
    ///     Project page id.
    /// </summary>
    public Guid PageId { get; set; }

    /// <summary>
    ///     Project page content.
    /// </summary>
    public virtual ProjectPage Page { get; set; }

    /// <summary>
    ///     Project tags.
    /// </summary>
    public virtual ICollection<Tag> Tags { get; private set; } = new List<Tag>();

    /// <summary>
    ///     Project team.
    /// </summary>
    public virtual ICollection<Teammate.Teammate> Users { get; set; } = new List<Teammate.Teammate>();

    /// <summary>
    ///     Problem of project.
    /// </summary>
    public string? Problem { get; set; }

    /// <summary>
    ///     Idea of project.
    /// </summary>
    public string? Idea { get; set; }

    /// <summary>
    ///     Problem of project.
    /// </summary>
    public string? Solution { get; set; }

    /// <summary>
    ///     Project completion status.
    /// </summary>
    public CompletionStatusEnum CompletionStatus { get; set; }

    /// <summary>
    ///     Type of initiative.
    /// </summary>
    public TypeInitiativeEnum TypeInitiative { get; set; }

    /// <summary>
    ///     A short description of the information that will be displayed on the project card.
    /// </summary>
    public string? TextPreview { get; set; }
}
