using System.ComponentModel.DataAnnotations;

namespace Vitrina.UseCases.User.DTO.Profile.Base;

public record UserDto
{
    /// <summary>
    ///     User first name.
    /// </summary>
    [StringLength(255, ErrorMessage = "The FirstName must be no more than 255 characters long.")]
    [RegularExpression(@"^(?!.*\d).+$", ErrorMessage = "The FirstName must not contain numbers and must not be empty.")]
    public string? FirstName { get; init; }

    /// <summary>
    ///     User last name.
    /// </summary>
    [StringLength(255, ErrorMessage = "The LastName must be no more than 255 characters long.")]
    [RegularExpression(@"^(?!.*\d).+$", ErrorMessage = "The LastName must not contain numbers and must not be empty.")]
    public string? LastName { get; init; }

    /// <summary>
    ///     User patronymic.
    /// </summary>
    [StringLength(255, ErrorMessage = "The Patronymic must be no more than 255 characters long.")]
    [RegularExpression(@"^(?!.*\d).+$",
        ErrorMessage = "The Patronymic must not contain numbers and must not be empty.")]
    public string? Patronymic { get; init; }

    /// <summary>
    ///     Telegram username of user.
    /// </summary>
    [RegularExpression("^[a-zA-Z][a-zA-Z0-9_-]{4,31}$", ErrorMessage = "This is an incorrect Telegram username.")]
    public string? Telegram { get; init; }

    /// <summary>
    ///     User email.
    /// </summary>
    [EmailAddress]
    [StringLength(100, ErrorMessage = "The Email must be no more than 100 characters long.")]
    public string? Email { get; init; }

    /// <summary>
    ///     User phone number.
    /// </summary>
    [RegularExpression(@"^\+7\d{10}$", ErrorMessage = "Incorrect phone number.")]
    public string? PhoneNumber { get; init; }
}
