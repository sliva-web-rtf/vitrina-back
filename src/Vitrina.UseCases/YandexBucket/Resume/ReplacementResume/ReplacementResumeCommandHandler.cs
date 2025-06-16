using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.YandexBucket.Resume.ReplacementResume;

public class ReplacementResumeCommandHandler(IS3StorageService s3Storage, IAppDbContext appDbContext)
    : IRequestHandler<ReplacementResumeCommand>
{
    private readonly List<string> allowedFormats = [".pdf"];

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

        var extension = Path.GetExtension(request.File.FileName);
        if (allowedFormats.All(allowedExtension => allowedExtension != extension))
        {
            throw new DomainException("Неправильный формат файла.");
        }

        var resume = appDbContext.Resumes.FirstOrDefault(resume => resume.Id == request.ResumeId)
                     ?? throw new NotFoundException("Резюме не найдено.");
        resume.File.ThrowExceptionIfNoAccess(request.IdAuthorizedUser);

        await ReplaceFile(resume, request, cancellationToken);
    }

    private async Task ReplaceFile(
        Domain.User.Resume resume,
        ReplacementResumeCommand request,
        CancellationToken cancellationToken
    )
    {
        await using var stream = request.File.OpenReadStream();
        var previousFilePath = resume.File.Path;
        var path = Path.Combine(Path.GetDirectoryName(previousFilePath),
            $"{Guid.NewGuid()}.{Path.GetExtension(previousFilePath)}");
        await s3Storage.SaveFileAsync(stream, path, request.File.ContentType, cancellationToken);
        resume.File.Path = path;

        try
        {
            await appDbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            await s3Storage.DeleteFileAsync(path, cancellationToken);
            await appDbContext.Files.Entry(resume.File).ReloadAsync(cancellationToken);
            throw;
        }

        await s3Storage.DeleteFileAsync(previousFilePath, cancellationToken);
    }
}
