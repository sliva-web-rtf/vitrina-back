using MediatR;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Domain.Project;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Project.YandexBucket.Image.SaveImage;

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

        await using var stream = request.File.OpenReadStream();
        var fileId = await s3Storage.SaveImageAsync(stream, request.Path + request.File.FileName,
            request.File.ContentType,
            cancellationToken);
        var url = s3Storage.GetFileUrl(fileId);
        var content = new Content { ImageUrl = url, Project = null };

        project.Contents.Add(content);
        appDbContext.Images.Add(new() { Id = fileId });

        await appDbContext.SaveChangesAsync(cancellationToken);
        return fileId;
    }
}
