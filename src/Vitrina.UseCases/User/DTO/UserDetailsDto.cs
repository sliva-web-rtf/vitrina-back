namespace Vitrina.UseCases.User.DTO;

/// <summary>
///     User details.
/// </summary>
public class UserDetailsDto
{
    /// <summary>
    ///     User identifier.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     User email.
    /// </summary>
    required public string Email { get; set; }

    /// <summary>
    ///     Last login date time.
    /// </summary>
    public DateTime LastLogin { get; set; }
}
