using MediatR;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Project.YandexBucket.SaveImage;

public class SaveImageCommandHandler(IS3StorageService s3Storage, IAppDbContext appDbContext)
    : IRequestHandler<SaveImageCommand, string>
{
    public async Task<string> Handle(SaveImageCommand request, CancellationToken ct)
    {
        await using var stream = request.File.OpenReadStream();
        var url = await s3Storage.SaveFileAsync(stream, request.File.FileName, request.File.ContentType);

        /*db.Images.Add(new ImageEntity { Url = url });
        await db.SaveChangesAsync(ct);*/

        return url;
    }
}
