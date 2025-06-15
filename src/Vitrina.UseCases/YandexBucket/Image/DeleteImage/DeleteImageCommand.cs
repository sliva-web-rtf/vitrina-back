using MediatR;

namespace Vitrina.UseCases.YandexBucket.Image.DeleteImage;

public record DeleteImageCommand(Guid Id, int IdAuthorizedUser) : IRequest;
