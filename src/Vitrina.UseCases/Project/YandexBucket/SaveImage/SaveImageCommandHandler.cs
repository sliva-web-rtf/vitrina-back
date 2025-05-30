using MediatR;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Domain.Project;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Project.YandexBucket.SaveImage;

public class SaveImageCommandHandler(IS3StorageService s3Storage, IAppDbContext appDbContext)
    : IRequestHandler<SaveImageCommand, string>
{
    private readonly List<(string ContentType, string Extension)> allowedFormats =
    [
        ("image/jpeg", "jpg"), ("image/png", "png"), ("image/jpeg", "jpeg"), ("image/webp", "webp")
    ];

    public async Task<string> Handle(SaveImageCommand request, CancellationToken cancellationToken)
    {
        if (request.File == null)
        {
            throw new DomainException("Попытка отправить пустой файл.");
        }

        await using var stream = request.File.OpenReadStream();

        var project = await appDbContext.Projects.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException("Project not found.");
        if (request.File.FileName.Split(".").Length < 2)
        {
            throw new DomainException("Неправильный формат картинки.");
        }

        var extension = request.File.FileName.Split(".").Last();
        if (!allowedFormats.Any(f => f.Extension == extension && f.ContentType == request.File.ContentType))
        {
            throw new DomainException("Неправильный формат картинки.");
        }

        var url = await s3Storage.SaveFileAsync(stream, request.path + request.File.FileName, request.File.ContentType,
            cancellationToken);
        var content = new Content { ImageUrl = url, Project = null };
        project.Contents.Add(content);

        await appDbContext.SaveChangesAsync(cancellationToken);
        return url;
    }
}
