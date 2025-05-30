using MediatR;
using Microsoft.AspNetCore.Http;

namespace Vitrina.UseCases.Project.YandexBucket.Image.SaveImage;

public record SaveImageCommand(IFormFile File, string Path, int Id) : IRequest<string>;
