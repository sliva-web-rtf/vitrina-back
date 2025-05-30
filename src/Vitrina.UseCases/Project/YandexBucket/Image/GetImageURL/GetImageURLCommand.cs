using MediatR;

namespace Vitrina.UseCases.Project.YandexBucket.Image.GetImageURL;

public record GetImageURLCommand(string FileName) : IRequest<string>;
