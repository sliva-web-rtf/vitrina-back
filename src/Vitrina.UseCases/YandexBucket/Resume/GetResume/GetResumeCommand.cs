using MediatR;
using Vitrina.UseCases.Project.YandexBucket.Resume.Dto;

namespace Vitrina.UseCases.YandexBucket.Resume.GetResume;

public record GetResumeCommand(Guid Id) : IRequest<ResumeDto>;
