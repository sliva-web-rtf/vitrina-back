using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Project.YandexBucket.Resume.GetFileURL;

public class GetResumeURLCommandHandler(IS3StorageService s3Storage, IAppDbContext appDbContext)
    : IRequestHandler<GetResumeURLCommand, string>
{
    public Task<string> Handle(GetResumeURLCommand request, CancellationToken cancellationToken)
    {
        var resume = appDbContext.Resume.FirstOrDefault(resume => resume.Id == request.Id);
        if (resume == null)
        {
            throw new NotFoundException("У пользователя нет резюме.");
        }
        return s3Storage.GetPreSignedURL(resume.FileName, request.Path,
            TimeSpan.FromHours(1));
    }
}
