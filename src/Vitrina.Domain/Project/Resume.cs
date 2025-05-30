namespace Vitrina.Domain.Project;

/// <summary>
/// ResumeDto.
/// </summary>
public class Resume
{
    /// <summary>
    /// Resume id
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// File name
    /// </summary>
    required public string FileName { get; set; }

    /// <summary>
    /// Resume id
    /// </summary>
    required public Guid UserId { get; set; }
}
