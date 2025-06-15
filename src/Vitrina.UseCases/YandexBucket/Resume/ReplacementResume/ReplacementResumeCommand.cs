using MediatR;
using Microsoft.AspNetCore.Http;

namespace Vitrina.UseCases.YandexBucket.Resume.ReplacementResume;

public record ReplacementResumeCommand(IFormFile File, Guid ResumeId, int IdAuthorizedUser) : IRequest;
