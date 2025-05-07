using MediatR;

namespace Vitrina.UseCases.Project.YandexBucket;

public record GetImageURLCommand(string fileName) : IRequest<string>;
