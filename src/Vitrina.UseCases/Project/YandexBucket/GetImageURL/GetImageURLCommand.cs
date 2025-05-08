using MediatR;

namespace Vitrina.UseCases.Project.YandexBucket.GetImageURL;

public record GetImageURLCommand(string FileName) : IRequest<string>;
