namespace Vitrina.Domain.User;

/// <summary>
///     Resume.
/// </summary>
public class Resume
{
    /// <summary>
    ///     Resume id
    /// </summary>
    required public Guid Id { get; init; }

    required public Guid FileId { get; init; }

    public virtual File File { get; init; }

    /// <summary>
    ///     User id
    /// </summary>
    required public int UserId { get; init; }

    /// <summary>
    ///     Navigation property to the user.
    /// </summary>
    public virtual User User { get; set; }
}
