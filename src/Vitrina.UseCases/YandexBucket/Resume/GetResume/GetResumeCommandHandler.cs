using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.UseCases.YandexBucket.Resume.Dto;

namespace Vitrina.UseCases.YandexBucket.Resume.GetResume;

public class GetResumeCommandHandler(IS3StorageService s3Storage, IAppDbContext dbContext)
    : IRequestHandler<GetResumeCommand, ResumeDto>
{
    public async Task<ResumeDto> Handle(GetResumeCommand request, CancellationToken cancellationToken)
    {
        var resume = await dbContext.Resumes.FindAsync(request.Id, cancellationToken)
                    ?? throw new NotFoundException($"Резюме с id = {request.Id} не найдено.");
        var url = await s3Storage.GetPreSignedURL(resume.File.Path, TimeSpan.FromHours(1));
        return new ResumeDto { Id = resume.Id, Url = url };
    }
}
