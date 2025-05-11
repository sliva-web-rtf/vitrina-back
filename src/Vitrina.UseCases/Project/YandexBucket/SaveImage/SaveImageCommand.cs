using MediatR;
using Microsoft.AspNetCore.Http;

namespace Vitrina.UseCases.Project.YandexBucket.SaveImage;

public record SaveImageCommand(IFormFile File, string path, int Id) : IRequest<string>;
