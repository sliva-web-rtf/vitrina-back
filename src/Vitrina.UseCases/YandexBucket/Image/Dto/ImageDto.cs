namespace Vitrina.UseCases.YandexBucket.Image.Dto;

public class ImageDto
{
    required public Guid Id { get; init; }

    required public string Url { get; init; }
}
