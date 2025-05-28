namespace Vitrina.UseCases.User.DTO;

public record RequestShortenedUserDto
{
    /// <summary>
    ///     User first name.
    /// </summary>
    public string FirstName { get; init; }

    /// <summary>
    ///     User last name.
    /// </summary>
    public string LastName { get; init; }

    /// <summary>
    ///     User patronymic.
    /// </summary>
    public string Patronymic { get; init; }

    /// <summary>
    ///     User email.
    /// </summary>
    public string Email { get; init; }
}
