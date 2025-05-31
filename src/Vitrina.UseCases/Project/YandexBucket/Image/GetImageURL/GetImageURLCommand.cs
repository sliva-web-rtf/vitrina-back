using MediatR;

namespace Vitrina.UseCases.Project.YandexBucket.Image.GetImageURL;

public record GetImageURLCommand(string FileName, string Path) : IRequest<string>;
