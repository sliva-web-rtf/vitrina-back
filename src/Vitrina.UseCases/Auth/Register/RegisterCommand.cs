using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Vitrina.UseCases.Auth.Register;

/// <summary>
/// Register user command.
/// </summary>
public class RegisterCommand : IRequest
{
    /// <summary>
    /// User email.
    /// </summary>
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    /// <summary>
    /// User password.
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    /// <summary>
    /// Confirm password.
    /// </summary>
    [Required]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    [DataType(DataType.Password)]
    public string PasswordConfirm { get; set; }

    /// <summary>
    /// Education level of user.
    /// </summary>
    required public string EducationLevel { get; set; }

    /// <summary>
    /// Education course of user.
    /// </summary>
    public int EducationCourse { get; set; }
}
