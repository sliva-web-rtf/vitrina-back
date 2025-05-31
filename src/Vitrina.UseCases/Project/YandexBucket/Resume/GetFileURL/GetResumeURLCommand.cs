using MediatR;

namespace Vitrina.UseCases.Project.YandexBucket.Resume.GetFileURL;

public record GetResumeURLCommand(int UserId, string Path) : IRequest<string>;
