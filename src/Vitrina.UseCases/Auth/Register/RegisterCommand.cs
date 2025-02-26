using System.ComponentModel.DataAnnotations;
using MediatR;
using Vitrina.Domain.User;

namespace Vitrina.UseCases.Auth.Register;

/// <summary>
/// Register user command.
/// </summary>
public class RegisterCommand : IRequest<RegisterCommandResult>
{
    /// <summary>
    /// User email.
    /// </summary>
    [Required]
    [EmailAddress]
    required public string Email { get; set; }

    /// <summary>
    /// User password.
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    required public string Password { get; set; }

    /// <summary>
    /// Confirm password.
    /// </summary>
    [Required]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    [DataType(DataType.Password)]
    required public string PasswordConfirm { get; set; }

    /// <summary>
    /// Education level of user.
    /// </summary>
    required public EducationLevelEnum EducationLevel { get; set; }

    /// <summary>
    /// Education course of user.
    /// </summary>
    public int EducationCourse { get; set; }

    /// <summary>
    /// User role in team.
    /// </summary>
    public RoleInTeamEnum RoleInTeam { get; set; }

    /// <summary>
    /// User first name.
    /// </summary>
    required public string FirstName { get; set; }

    /// <summary>
    /// User last name.
    /// </summary>
    required public string LastName { get; set; }

    /// <summary>
    /// User surname.
    /// </summary>
    required public string Surname { get; set; }
}
