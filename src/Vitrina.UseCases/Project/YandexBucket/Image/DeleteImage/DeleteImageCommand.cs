using MediatR;

namespace Vitrina.UseCases.Project.YandexBucket.Image.DeleteImage;

public record DeleteImageCommand(string FileName, string Path) : IRequest;
