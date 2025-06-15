using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.UseCases.YandexBucket.Image.Dto;

namespace Vitrina.UseCases.YandexBucket.Image.GetImage;

public class GetImageCommandHandler(IS3StorageService s3Storage, IAppDbContext dbContext)
    : IRequestHandler<GetImageCommand, ImageDto>
{
    public async Task<ImageDto> Handle(GetImageCommand request, CancellationToken cancellationToken)
    {
        var image = await dbContext.Images.FindAsync(request.Id)
                    ?? throw new NotFoundException($"Изоображение с id = {request.Id} не найдено.");
        var path = Path.GetFileName(image.File.Path);
        var url = await s3Storage.GetPreSignedURL(path, TimeSpan.FromHours(1));
        return new ImageDto { Id = image.Id, Url = url };
    }
}
