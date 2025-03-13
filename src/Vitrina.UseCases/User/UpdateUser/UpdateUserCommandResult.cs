namespace Vitrina.Domain.User;

/// <summary>
/// The result of updating user data.
/// </summary>
public record UpdateUserCommandResult(bool IsSuccess = true, string? ErrorMessage = null);
