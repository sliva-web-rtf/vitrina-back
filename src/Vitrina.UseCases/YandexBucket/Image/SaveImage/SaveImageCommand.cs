using MediatR;
using Microsoft.AspNetCore.Http;

namespace Vitrina.UseCases.YandexBucket.Image.SaveImage;

public record SaveImageCommand(IFormFile File, string Path, int IdAuthorizedUser) : IRequest<Guid>;
