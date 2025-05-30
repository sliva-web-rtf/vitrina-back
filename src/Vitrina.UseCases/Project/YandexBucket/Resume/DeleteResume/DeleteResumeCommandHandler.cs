using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Project.YandexBucket.Resume.DeleteResume;

public class DeleteResumeCommandHandler(IS3StorageService s3Storage, IAppDbContext appDbContext)
    : IRequestHandler<DeleteResumeCommand>
{
    public async Task Handle(DeleteResumeCommand request, CancellationToken cancellationToken)
    {
        var currentUser = appDbContext.Users.FirstOrDefault(user => user.Id == request.UserId) ??
            throw new DomainException("Такого пользователя не существует.");

        await s3Storage.DeleteFileAsync(currentUser.Resume!.FileName, cancellationToken);
        currentUser.Resume = null;
        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}
