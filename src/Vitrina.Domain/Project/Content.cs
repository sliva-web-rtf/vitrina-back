using System.ComponentModel.DataAnnotations;

namespace Saritasa.RedMan.Domain.Project;

/// <summary>
/// Project content.
/// </summary>
public class Content
{
    /// <summary>
    /// Content id.
    /// </summary>
    [Key]
    public int Id { get; private set; }

    /// <summary>
    /// Bytes of image.
    /// </summary>
    required public byte[] ImageBytes { get; set; }

    /// <summary>
    /// Project id.
    /// </summary>
    public int ProjectId { get; private set; }

    /// <summary>
    /// Project.
    /// </summary>
    required public Project Project { get; set; }
}
