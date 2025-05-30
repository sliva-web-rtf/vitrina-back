using MediatR;

namespace Vitrina.UseCases.Project.YandexBucket.Resume.DeleteResume;

public record DeleteResumeCommand(int UserId) : IRequest;
