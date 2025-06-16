namespace Vitrina.UseCases.Common.DTO;

public record FileDto
{
    required public Guid Id { get; init; }

    required public string Url { get; init; }
}
