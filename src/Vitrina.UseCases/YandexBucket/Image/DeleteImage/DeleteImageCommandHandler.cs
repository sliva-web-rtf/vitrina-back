using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.YandexBucket.Image.DeleteImage;

public class DeleteImageCommandHandler(IS3StorageService s3Storage, IAppDbContext appDbContext)
    : IRequestHandler<DeleteImageCommand>
{
    public async Task Handle(DeleteImageCommand request, CancellationToken cancellationToken)
    {
        var image = await appDbContext.Images.FindAsync(request.Id, cancellationToken)
                    ?? throw new NotFoundException("Изоображение не найдено");
        image.File.ThrowExceptionIfNoAccess(request.IdAuthorizedUser);
        appDbContext.Images.Remove(image);
        appDbContext.Files.Remove(image.File);
        await appDbContext.SaveChangesAsync(cancellationToken);
        await s3Storage.DeleteFileAsync(image.File.Path, cancellationToken);
    }
}
