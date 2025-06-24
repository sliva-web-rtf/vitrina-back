using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Project.YandexBucket.Resume.ReplacementResume;

public class ReplacementResumeCommandHandler(IS3StorageService s3Storage, IAppDbContext appDbContext)
    : IRequestHandler<ReplacementResumeCommand>
{
    private readonly List<string> allowedFormats = ["pdf"];

    public async Task Handle(ReplacementResumeCommand request, CancellationToken cancellationToken)
    {
        if (request.File == null)
        {
            throw new DomainException("Попытка отправить пустой файл.");
        }

        if (request.File.FileName.Split(".").Length < 2)
        {
            throw new DomainException("Неправильный формат файла.");
        }

        var extension = request.File.FileName.Split(".").Last();
        if (allowedFormats.All(ext => ext != extension))
        {
            throw new DomainException("Неправильный формат файла.");
        }

        var resume = appDbContext.Resume.FirstOrDefault(resume => resume.Id == request.Id);
        if (resume == null)
        {
            throw new NotFoundException("У пользователя нет резюме.");
        }

        await s3Storage.DeleteFileAsync(resume.FileName, request.Path, cancellationToken);
        appDbContext.Resume.Remove(resume);

        await using var stream = request.File.OpenReadStream();
        var fileName = Guid.NewGuid() + ".pdf";
        await s3Storage.SaveFileAsync(stream, fileName, request.Path,
            request.File.ContentType,
            cancellationToken);

        resume = new() { UserId = resume.UserId, FileName = fileName, User = resume.User };
        appDbContext.Resume.Add(resume);

        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}
