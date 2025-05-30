using MediatR;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Project.YandexBucket.DeleteImage;

public class DeleteImageCommandHandler(IS3StorageService s3Storage) : IRequestHandler<DeleteImageCommand>
{
    public Task Handle(DeleteImageCommand request, CancellationToken cancellationToken)
    {
        return s3Storage.DeleteFileAsync(request.FileName, cancellationToken);
    }
}
