using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Domain.Project;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Project.UploadImages;

/// <summary>
/// Upload images handler.
/// </summary>
internal class UploadImagesCommandHandler : IRequestHandler<UploadImagesCommand>
{
    private readonly IHostingEnvironment hostingEnvironment;
    private readonly IAppDbContext appDbContext;

    public UploadImagesCommandHandler(IHostingEnvironment hostingEnvironment, IAppDbContext appDbContext)
    {
        this.hostingEnvironment = hostingEnvironment;
        this.appDbContext = appDbContext;
    }

    public async Task Handle(UploadImagesCommand request, CancellationToken cancellationToken)
    {
        var project = await appDbContext.Projects.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException("Project not found.");
        foreach (var file in request.Files)
        {
            if (file.FileName.Split(".").Length < 2)
            {
                throw new DomainException("Неправильный формат картинки.");
            }

            var extension = file.FileName.Split(".").Last();
            var allowedFormats =
                new List<(string ContentType, string Extension)> { ("image/jpeg", "jpg"), ("image/png", "png"), ("image/jpeg", "jpeg") };
            if (!allowedFormats.Any(f => f.Extension == extension && f.ContentType == file.ContentType))
            {
                throw new DomainException("Неправильный формат картинки.");
            }

            var webRootDirectory = hostingEnvironment.WebRootPath.TrimEnd('/');
            var path = request.IsAvatar ? $"/Avatars/{Guid.NewGuid()}.{extension}" : $"/Preview/{Guid.NewGuid()}.{extension}";
            var filePath = webRootDirectory + path;

            using var fileStream = File.Create(filePath);
            file.Data.Seek(0, SeekOrigin.Begin);
            await file.Data.CopyToAsync(fileStream, cancellationToken);

            if (request.IsAvatar)
            {
                var content = new Content() { ImageUrl = filePath, Project = project };
                project.Contents.Add(content);
            }
            else
            {
                project.PreviewImagePath = filePath;
            }

            await file.Data.DisposeAsync();
        }

        await appDbContext.SaveChangesAsync(cancellationToken);
    }
}
