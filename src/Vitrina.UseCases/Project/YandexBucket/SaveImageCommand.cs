using MediatR;
using Microsoft.AspNetCore.Http;

namespace Vitrina.UseCases.Project.YandexBucket;

public record SaveImageCommand(IFormFile File) : IRequest<string>;
