using MediatR;
using Microsoft.AspNetCore.Http;

namespace Vitrina.UseCases.YandexBucket.Resume.SaveResume;

public record SaveResumeCommand(IFormFile File, string Path, int IdAuthorizedUser) : IRequest<Guid>;
