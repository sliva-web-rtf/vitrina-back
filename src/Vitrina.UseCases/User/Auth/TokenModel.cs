namespace Vitrina.UseCases.Auth;

/// <summary>
///     API generated token model.
/// </summary>
public class TokenModel
{
    /// <summary>
    ///     Token.
    /// </summary>
    required public string Token { get; set; }

    /// <summary>
    ///     Token expiration in seconds.
    /// </summary>
    public long ExpiresIn { get; set; }
}
