using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Vitrina.UseCases.User.Auth.ResetPassword;

public class ResetPasswordCommand : IRequest<ResetPasswordCommandResult>
{
    [Required]
    public string Token { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    public string ConfirmPassword { get; set; }
}
