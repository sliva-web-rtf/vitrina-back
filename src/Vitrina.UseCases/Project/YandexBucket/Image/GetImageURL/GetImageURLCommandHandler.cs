using MediatR;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Project.YandexBucket.Image.GetImageURL;

public class GetImageURLCommandHandler(IS3StorageService s3Storage)
    : IRequestHandler<GetImageURLCommand, string>
{
    public Task<string> Handle(GetImageURLCommand request, CancellationToken cancellationToken)
    {
        return s3Storage.GetPreSignedURL(request.FileName, request.Path, TimeSpan.FromHours(1));
    }
}
