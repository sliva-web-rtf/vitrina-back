namespace Vitrina.UseCases.Email;

public class EmailSettings
{
    public const string SectionName = "Mail";

    public string FromAddress { get; init; } = null!;

    public string Username { get; init; } = null!;

    public string Password { get; init; } = null!;

    public string Host { get; init; } = null!;
}
