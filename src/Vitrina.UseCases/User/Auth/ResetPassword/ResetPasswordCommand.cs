using System.ComponentModel.DataAnnotations;
using Destructurama.Attributed;
using MediatR;

namespace Vitrina.UseCases.User.Auth.ResetPassword;

public class ResetPasswordCommand : IRequest<ResetPasswordCommandResult>
{
    [Required]
    public string Token { get; set; }

    [Required]
    [EmailAddress]
    [LogMasked]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [LogMasked]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    [LogMasked]
    public string ConfirmPassword { get; set; }
}
