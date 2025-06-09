using MediatR;

namespace Vitrina.UseCases.Project.YandexBucket.Resume.GetFileURL;

public record GetResumeURLCommand(Guid Id, string Path) : IRequest<string>;
