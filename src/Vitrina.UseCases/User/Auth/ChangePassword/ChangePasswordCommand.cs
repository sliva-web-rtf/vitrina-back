using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Vitrina.UseCases.User.Auth.ChangePassword;

public class ChangePasswordCommand : IRequest<ChangePasswordCommandResult>
{
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
