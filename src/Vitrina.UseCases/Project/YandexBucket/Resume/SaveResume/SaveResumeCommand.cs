using MediatR;
using Microsoft.AspNetCore.Http;

namespace Vitrina.UseCases.Project.YandexBucket.Resume.SaveResume;

public record SaveResumeCommand(IFormFile File, string Path, int UserId) : IRequest;
