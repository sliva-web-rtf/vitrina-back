using MediatR;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Project.YandexBucket;

public class GetImageURLCommandHandler(IS3StorageService s3Storage, IAppDbContext appDbContext)
    : IRequestHandler<GetImageURLCommand, string>
{
    public Task<string> Handle(GetImageURLCommand request, CancellationToken cancellationToken)
    {
        return s3Storage.GetFileURLAsync(request.fileName, TimeSpan.FromHours(1));
    }
}
