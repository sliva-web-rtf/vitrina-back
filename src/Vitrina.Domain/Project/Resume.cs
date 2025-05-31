namespace Vitrina.Domain.Project;

/// <summary>
/// Resume.
/// </summary>
public class Resume
{
    /// <summary>
    /// Resume id
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// User id
    /// </summary>
    required public int UserId { get; set; }

    /// <summary>
    /// The name of the resume file in the repository.
    /// </summary>
    required public string FileName { get; set; }

    /// <summary>
    /// Navigation property to the user.
    /// </summary>
    public User.User User { get; set; } = null!;
}
