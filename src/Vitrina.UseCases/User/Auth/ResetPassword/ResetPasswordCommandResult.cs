namespace Vitrina.UseCases.User.Auth.ResetPassword;

public record ResetPasswordCommandResult
{
    /// <summary>
    ///     Is success email confirmation.
    /// </summary>
    public bool IsSuccess { get; init; }

    /// <summary>
    ///     User id.
    /// </summary>
    public int UserId { get; init; }
}
