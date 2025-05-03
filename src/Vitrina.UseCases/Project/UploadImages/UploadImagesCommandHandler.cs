using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Webp;
using Vitrina.Domain.Project;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Project.UploadImages;

/// <summary>
/// Upload images handler.
/// </summary>
internal class UploadImagesCommandHandler : IRequestHandler<UploadImagesCommand>
{
    private readonly List<(string ContentType, string Extension)> allowedFormats = new()
    {
        ("image/jpeg", "jpg"),
        ("image/png", "png"),
        ("image/jpeg", "jpeg"),
        ("image/webp", "webp")
    };

    private readonly IHostingEnvironment hostingEnvironment1;
    private readonly IAppDbContext appDbContext1;

    /// <summary>
    /// Upload images handler.
    /// </summary>
    public UploadImagesCommandHandler(IHostingEnvironment hostingEnvironment, IAppDbContext appDbContext)
    {
        hostingEnvironment1 = hostingEnvironment;
        appDbContext1 = appDbContext;
    }

    public async Task Handle(UploadImagesCommand request, CancellationToken cancellationToken)
    {
        var project = await appDbContext1.Projects.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException("Project not found.");
        foreach (var file in request.Files)
        {
            if (file == null)
            {
                throw new DomainException("Попытка отправить пустой файл.");
            }

            if (file.FileName.Split(".").Length < 2)
            {
                throw new DomainException("Неправильный формат картинки.");
            }

            var extension = file.FileName.Split(".").Last();
            if (!allowedFormats.Any(f => f.Extension == extension && f.ContentType == file.ContentType))
            {
                throw new DomainException("Неправильный формат картинки.");
            }

            var basePath = Path.Combine(hostingEnvironment1.WebRootPath, request.IsAvatar ? "Avatars" : "Preview");
            var filePath = Path.Combine(basePath, $"{Guid.NewGuid()}.{extension}");
            var webpFilePath = Path.Combine(basePath,  $"{Guid.NewGuid()}.webp");

            await using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                file.Data.Seek(0, SeekOrigin.Begin);
                await file.Data.CopyToAsync(fileStream, cancellationToken);
            }

            using (var image = await Image.LoadAsync(filePath, cancellationToken))
            {
                await image.SaveAsWebpAsync(webpFilePath, new WebpEncoder()
                {
                    Method = WebpEncodingMethod.Default
                }, cancellationToken);
            }

            File.Delete(filePath);

            if (request.IsAvatar)
            {
                var content = new Content() { ImageUrl = webpFilePath, Project = project };
                project.Contents.Add(content);
            }
            else
            {
                project.PreviewImagePath = webpFilePath;
            }

            await file.Data.DisposeAsync();
        }

        await appDbContext1.SaveChangesAsync(cancellationToken);
    }
}
