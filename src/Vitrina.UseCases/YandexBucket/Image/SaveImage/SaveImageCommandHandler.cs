using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using SixLabors.ImageSharp;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using File = Vitrina.Domain.File;

namespace Vitrina.UseCases.YandexBucket.Image.SaveImage;

public class SaveImageCommandHandler(IS3StorageService s3Storage, IAppDbContext appDbContext)
    : IRequestHandler<SaveImageCommand, Guid>
{
    private readonly List<(string ContentType, string Extension)> allowedFormats =
    [
        ("image/jpeg", ".jpg"),
        ("image/png", ".png"),
        ("image/jpeg", ".jpeg"),
        ("image/webp", ".webp")
    ];

    public async Task<Guid> Handle(SaveImageCommand request, CancellationToken cancellationToken)
    {
        if (request.File == null)
        {
            throw new DomainException("Попытка отправить пустой файл.");
        }

        if (request.File.FileName.Split(".").Length < 2)
        {
            throw new DomainException("Неправильный формат картинки.");
        }

        var extension = Path.GetExtension(request.File.FileName);
        if (!allowedFormats.Any(format =>
                format.Extension == extension && format.ContentType == request.File.ContentType))
        {
            throw new DomainException("Неправильный формат картинки.");
        }

        var image = await SaveImageAsync(
            request,
            cancellationToken);
        return image.Id;
    }

    private async Task<Domain.Image> SaveImageAsync(
        SaveImageCommand request,
        CancellationToken cancellationToken
    )
    {
        var path = Path.Combine(request.Path, $"{Guid.NewGuid()}.webp");
        await using var fileStream = request.File.OpenReadStream();
        using var image = await SixLabors.ImageSharp.Image.LoadAsync(fileStream, cancellationToken);

        await using var webpStream = new MemoryStream();
        await image.SaveAsWebpAsync(webpStream, cancellationToken);

        webpStream.Seek(0, SeekOrigin.Begin);

        await s3Storage.SaveFileAsync(webpStream, path, request.File.ContentType, cancellationToken);
        var file = new File { Id = Guid.NewGuid(), Path = path, CreatorId = request.IdAuthorizedUser };
        var result = new Domain.Image { Id = Guid.NewGuid(), FileId = file.Id };

        try
        {
            appDbContext.Files.Add(file);
            appDbContext.Images.Add(result);
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
