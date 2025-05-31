using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Project.YandexBucket.Resume.DeleteResume;

public class DeleteResumeCommandHandler(IS3StorageService s3Storage, IAppDbContext appDbContext)
    : IRequestHandler<DeleteResumeCommand>
{
    public async Task Handle(DeleteResumeCommand request, CancellationToken cancellationToken)
    {
        if (appDbContext.Users.FirstOrDefault(user => user.Id == request.UserId) == null)
        {
            throw new DomainException("Такого пользователя не существует.");
        }

        var resume = appDbContext.Resume.FirstOrDefault(resume => resume.UserId == request.UserId);
        if (resume == null)
        {
            throw new DomainException("У пользователя нет резюме.");
        }

        await s3Storage.DeleteFileAsync(resume.FileName, request.Path, cancellationToken);
        appDbContext.Resume.Remove(resume);
        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}
