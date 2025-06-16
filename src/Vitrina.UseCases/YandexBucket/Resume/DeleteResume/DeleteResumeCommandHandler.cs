using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.YandexBucket.Resume.DeleteResume;

public class DeleteResumeCommandHandler(IS3StorageService s3Storage, IAppDbContext appDbContext)
    : IRequestHandler<DeleteResumeCommand>
{
    public async Task Handle(DeleteResumeCommand request, CancellationToken cancellationToken)
    {
        var resume = await appDbContext.Resumes.FindAsync(request.ResumeId, cancellationToken)
                     ?? throw new NotFoundException("Резюме не существует.");
        resume.File.ThrowExceptionIfNoAccess(request.IdAuthorizedUser);
        appDbContext.Resumes.Remove(resume);
        await appDbContext.SaveChangesAsync(cancellationToken);
        await s3Storage.DeleteFileAsync(resume.File.Path, cancellationToken);
    }
}
