using MediatR;
using Newtonsoft.Json.Linq;
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
        var user = await appDbContext.Users.FindAsync(request.IdAuthorizedUser, cancellationToken)
                   ?? throw new NotFoundException("Авторизированный пользователь не найден.");
        resume.File.ThrowExceptionIfNoAccess(request.IdAuthorizedUser);
        var additionalInformationJson = JObject.Parse(user.AdditionalInformation);
        additionalInformationJson["ResumeId"] = null;
        user.AdditionalInformation = additionalInformationJson.ToString();
        appDbContext.Resumes.Remove(resume);
        appDbContext.Files.Remove(resume.File);
        await appDbContext.SaveChangesAsync(cancellationToken);
        await s3Storage.DeleteFileAsync(resume.File.Path, cancellationToken);
    }
}
