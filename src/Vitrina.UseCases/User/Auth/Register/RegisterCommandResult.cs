namespace Vitrina.UseCases.User.Auth.Register;

/// <summary>
///     Result of <see cref="RegisterCommand" />.
/// </summary>
public class RegisterCommandResult
{
    /// <summary>
    ///     IsSuccess.
    /// </summary>
    public bool IsSuccess { get; init; }

    /// <summary>
    ///     Message
    /// </summary>
    public string? Message { get; init; }

    /// <summary>
    ///     Only for test.
    /// </summary>
    public string ConfirmationCode { get; set; }

    /// <summary>
    ///     Only for test.
    /// </summary>
    public int UserId { get; init; }
}
