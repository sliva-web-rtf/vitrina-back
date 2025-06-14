using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Project.YandexBucket.Resume.DeleteResume;

public class DeleteResumeCommandHandler(IS3StorageService s3Storage, IAppDbContext appDbContext)
    : IRequestHandler<DeleteResumeCommand>
{
    public async Task Handle(DeleteResumeCommand request, CancellationToken cancellationToken)
    {
        var resume = appDbContext.Resume.FirstOrDefault(resume => resume.Id == request.Id);
        if (resume == null)
        {
            throw new NotFoundException("У пользователя нет резюме.");
        }

        await s3Storage.DeleteFileAsync(resume.FileName, request.Path, cancellationToken);
        appDbContext.Resume.Remove(resume);
        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}
