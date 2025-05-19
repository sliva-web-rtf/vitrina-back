namespace Vitrina.UseCases.Common.DTO;

public record ShortenedUserDto
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
