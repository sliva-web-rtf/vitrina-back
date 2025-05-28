namespace Vitrina.UseCases.User.DTO;

public record ResponceShortenedUserDto : RequestShortenedUserDto
{
    public string? Avatar { get; init; }
}
