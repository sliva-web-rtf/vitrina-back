using MediatR;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Project.YandexBucket.Image.DeleteImage;

public class DeleteImageCommandHandler(IS3StorageService s3Storage, IAppDbContext appDbContext)
    : IRequestHandler<DeleteImageCommand>
{
    public async Task Handle(DeleteImageCommand request, CancellationToken cancellationToken)
    {
        appDbContext.Images.Remove(
            await appDbContext.Images.FindAsync([request.FileName, cancellationToken], cancellationToken));
        await appDbContext.SaveChangesAsync(cancellationToken);
        await s3Storage.DeleteFileAsync(request.FileName, cancellationToken);
    }
}
