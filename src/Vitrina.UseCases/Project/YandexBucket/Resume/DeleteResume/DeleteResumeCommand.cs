using MediatR;

namespace Vitrina.UseCases.Project.YandexBucket.Resume.DeleteResume;

public record DeleteResumeCommand(Guid Id, string Path) : IRequest;
