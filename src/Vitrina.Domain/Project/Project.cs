using System.ComponentModel.DataAnnotations;

namespace Vitrina.Domain.Project;

/// <summary>
/// Domain class of project.
/// </summary>
public class Project
{
    /// <summary>
    /// Project id.
    /// </summary>
    [Key]
    public int Id { get; private set; }

    /// <summary>
    /// Project name.
    /// </summary>
    required public string Name { get; set; }

    /// <summary>
    /// Project description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Project aim.
    /// </summary>
    public string? Aim { get; set; }

    /// <summary>
    /// Project client.
    /// </summary>
    public string? Client { get; set; }

    /// <summary>
    /// Semester.
    /// </summary>
    public SemesterEnum Semester { get; set; }

    /// <summary>
    /// Period.
    /// </summary>
    required public string Period { get; set; }

    /// <summary>
    /// Markdown of page.
    /// </summary>
    public string? Markdown { get; set; }

    /// <summary>
    /// Url of project video.
    /// </summary>
    public string? VideoUrl { get; set; }

    /// <summary>
    /// Priority of project.
    /// </summary>
    public int Priority { get; set; }

    /// <summary>
    /// Path to preview image.
    /// </summary>
    public string? PreviewImagePath { get; set; }

    /// <summary>
    /// Project content.
    /// </summary>
    public virtual ICollection<Content> Contents { get; private set; } = new List<Content>();

    /// <summary>
    /// Project tags.
    /// </summary>
    public virtual ICollection<Tag> Tags { get; private set; } = new List<Tag>();

    /// <summary>
    /// Project team.
    /// </summary>
    public virtual ICollection<Teammate> Users { get; set; } = new List<Teammate>();

    /// <summary>
    /// Problem of project.
    /// </summary>
    public string? Problem { get; set; }

    /// <summary>
    /// Idea of project.
    /// </summary>
    public string? Idea { get; set; }

    /// <summary>
    /// Problem of project.
    /// </summary>
    public string? Solution { get; set; }

    /// <summary>
    /// List of custom blocks.
    /// </summary>
    public virtual ICollection<Block> CustomBlocks { get; set; } = new List<Block>();

    /// <summary>
    /// Project sphere.
    /// </summary>
    public string? Sphere { get; set; }

    /// <summary>
    /// Project type.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Project completion status.
    /// </summary>
    public CompletionStatusEnum CompletionStatus { get; set; }

    /// <summary>
    /// Type of initiative.
    /// </summary>
    public TypeInitiativeEnum TypeInitiative { get; set; }

    /// <summary>
    /// A short description of the information that will be displayed on the project card.
    /// </summary>
    public string? TextPreview { get; set; }
}
