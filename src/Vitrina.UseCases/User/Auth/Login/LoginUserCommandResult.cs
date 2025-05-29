namespace Vitrina.UseCases.User.Auth.Login;

/// <summary>
///     Represents user login attempt to system.
/// </summary>
public class LoginUserCommandResult
{
    /// <summary>
    ///     Logged user id (if success).
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    ///     New refresh token.
    /// </summary>
    required public TokenModel TokenModel { get; set; }
}
