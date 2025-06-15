using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using File = Vitrina.Domain.File;

namespace Vitrina.UseCases.YandexBucket.Resume.SaveResume;

public class SaveResumeCommandHandler(IS3StorageService s3Storage, IAppDbContext appDbContext)
    : IRequestHandler<SaveResumeCommand, Guid>
{
    private readonly List<string> allowedFormats = [".pdf"];

    public async Task<Guid> Handle(SaveResumeCommand request, CancellationToken cancellationToken)
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

        var resume = appDbContext.Resumes.FirstOrDefault(resume => resume.UserId == request.IdAuthorizedUser);
        if (resume != null)
        {
            throw new ConflictException("У пользователя уже есть резюме.");
        }

        return (await SaveResumeAsync(request, cancellationToken)).Id;
    }

    private async Task<Domain.User.Resume> SaveResumeAsync(
        SaveResumeCommand request,
        CancellationToken cancellationToken
    )
    {
        var path = Path.Combine(request.Path, $"{Guid.NewGuid()}.pdf");
        await using var stream = request.File.OpenReadStream();
        await s3Storage.SaveFileAsync(stream, path, request.File.ContentType, cancellationToken);
        var file = new File { Id = Guid.NewGuid(), Path = path, CreatorId = request.IdAuthorizedUser };
        var result = new Domain.User.Resume
        {
            Id = Guid.NewGuid(), FileId = file.Id, UserId = request.IdAuthorizedUser
        };

        try
        {
            appDbContext.Files.Add(file);
            appDbContext.Resumes.Add(result);
            await appDbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            await s3Storage.DeleteFileAsync(path, cancellationToken);
            throw;
        }

        return result;
    }
}
