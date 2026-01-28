using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Vitrina.UseCases.User.Auth.ConfirmEmail;

/// <summary>
///     Confirm email command.
/// </summary>
public class ConfirmEmailCommand : IRequest<ConfirmEmailCommandResult>
{
    /// <summary>
    ///     User id.
    /// </summary>
    public int UserId { get; init; }

    /// <summary>
    ///     Confirmation code.
    /// </summary>
    [DataType(DataType.Password)]
    public string ConfirmationCode { get; init; }
}
