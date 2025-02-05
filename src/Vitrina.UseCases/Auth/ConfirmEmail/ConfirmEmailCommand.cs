using MediatR;

namespace Vitrina.UseCases.Auth.ConfirmEmail;

/// <summary>
/// Confirm email command.
/// </summary>
public class ConfirmEmailCommand : IRequest<ConfirmEmailCommandResult>
{
    /// <summary>
    /// User id.
    /// </summary>
    public int UserId { get; init; }

    /// <summary>
    /// Confirmation code.
    /// </summary>
    public int ConfirmationCode { get; init; }
}
