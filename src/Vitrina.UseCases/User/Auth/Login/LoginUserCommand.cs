using System.ComponentModel.DataAnnotations;
using Destructurama.Attributed;
using MediatR;

namespace Vitrina.UseCases.User.Auth.Login;

/// <summary>
///     Login user command.
/// </summary>
public record LoginUserCommand : IRequest<LoginUserCommandResult>
{
    /// <summary>
    ///     Email.
    /// </summary>
    [EmailAddress]
    [Required]
    [DataType(DataType.EmailAddress)]
    [LogMasked]
    required public string Email { get; init; }

    /// <summary>
    ///     Password.
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    [LogMasked]
    required public string Password { get; init; }

    /// <summary>
    ///     Remember user's cookie for longer period.
    /// </summary>
    public bool RememberMe { get; init; }
}
