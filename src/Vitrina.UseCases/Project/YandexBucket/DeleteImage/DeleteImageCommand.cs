using MediatR;

namespace Vitrina.UseCases.Project.YandexBucket.DeleteImage;

public record DeleteImageCommand(string FileName) : IRequest;
