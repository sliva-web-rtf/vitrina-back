using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Project.YandexBucket.Resume.ReplacementResume;

public class ReplacementResumeCommandHandler(IS3StorageService s3Storage, IAppDbContext appDbContext)
    : IRequestHandler<ReplacementResumeCommand>
{
    private readonly List<(string ContentType, string Extension)> allowedFormats = [("resume/pdf", "pdf")];

    public async Task Handle(ReplacementResumeCommand request, CancellationToken cancellationToken)
    {
        if (request.File == null)
        {
            throw new DomainException("Попытка отправить пустой файл.");
        }

        var currentUser = appDbContext.Users.FirstOrDefault(user => user.Id == request.UserId) ??
            throw new DomainException("Такого пользователя не существует.");

        if (request.File.FileName.Split(".").Length < 2)
        {
            throw new DomainException("Неправильный формат файла.");
        }

        var extension = request.File.FileName.Split(".").Last();
        if (!allowedFormats.Any(f => f.Extension == extension && f.ContentType == request.File.ContentType))
        {
            throw new DomainException("Неправильный формат файла.");
        }

        await s3Storage.DeleteFileAsync(currentUser.Resume!.FileName, cancellationToken);
        currentUser.Resume = null;

        await using var stream = request.File.OpenReadStream();
        var fileName = request.Path + Guid.NewGuid() + ".pdf";
        await s3Storage.SaveFileAsync(stream, fileName,
            request.File.ContentType,
            cancellationToken);

        currentUser.Resume = new() { UserId = request.UserId, FileName = fileName, User = currentUser };

        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}
