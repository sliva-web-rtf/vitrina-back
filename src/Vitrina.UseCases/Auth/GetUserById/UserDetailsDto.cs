namespace Vitrina.UseCases.Auth.GetUserById;

/// <summary>
/// User details.
/// </summary>
public class UserDetailsDto
{
    /// <summary>
    /// User identifier.
    /// </summary>
    required public int Id { get; set; }

    /// <summary>
    /// User email.
    /// </summary>
    required public string Email { get; set; }
}
