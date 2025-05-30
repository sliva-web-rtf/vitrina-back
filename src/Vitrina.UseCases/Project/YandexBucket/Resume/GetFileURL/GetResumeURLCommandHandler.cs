using MediatR;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Project.YandexBucket.Resume.GetFileURL;

public class GetResumeURLCommandHandler(IS3StorageService s3Storage, IAppDbContext appDbContext)
    : IRequestHandler<GetResumeURLCommand, string>
{
    public Task<string> Handle(GetResumeURLCommand request, CancellationToken cancellationToken)
    {
        return s3Storage.GetPreSignedURL(
            appDbContext.Users.FirstOrDefault(user => user.Id == request.UserId)!.Resume!.FileName,
            TimeSpan.FromHours(1));
    }
}
