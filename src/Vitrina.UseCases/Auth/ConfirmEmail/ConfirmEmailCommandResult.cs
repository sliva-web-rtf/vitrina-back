namespace Vitrina.UseCases.Auth.ConfirmEmail;

/// <summary>
/// Result of <see cref="ConfirmEmailCommand"/>.
/// </summary>
public class ConfirmEmailCommandResult
{
    /// <summary>
    /// Is success email confirmation.
    /// </summary>
    public bool IsSuccess { get; init; }

    /// <summary>
    /// Message.
    /// </summary>
    public string? Message { get; init; }

    /// <summary>
    /// User id.
    /// </summary>
    public int UserId { get; init; }

    /// <summary>
    /// Token.
    /// </summary>
    public TokenModel? Token { get; init; }
}
