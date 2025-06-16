using MediatR;

namespace Vitrina.UseCases.YandexBucket.Resume.DeleteResume;

public record DeleteResumeCommand(Guid ResumeId, int IdAuthorizedUser) : IRequest;
