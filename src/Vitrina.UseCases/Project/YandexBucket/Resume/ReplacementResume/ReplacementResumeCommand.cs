using MediatR;
using Microsoft.AspNetCore.Http;

namespace Vitrina.UseCases.Project.YandexBucket.Resume.ReplacementResume;

public record ReplacementResumeCommand(IFormFile File, string Path, int UserId) : IRequest;
