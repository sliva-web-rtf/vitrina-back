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
    /// Project content.
    /// </summary>
    public ICollection<Content> Contents { get; private set; } = new List<Content>();

    /// <summary>
    /// Project tags.
    /// </summary>
    public ICollection<Tag> Tags { get; private set; } = new List<Tag>();

    /// <summary>
    /// Project team.
    /// </summary>
    public ICollection<User> Users { get; private set; } = new List<User>();
}
