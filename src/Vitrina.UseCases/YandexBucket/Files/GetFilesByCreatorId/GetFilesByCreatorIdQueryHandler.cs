using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.UseCases.Common.DTO;

namespace Vitrina.UseCases.YandexBucket.Files.GetFilesByCreatorId;

public class GetFilesByCreatorIdQueryHandler(IAppDbContext dbContext, IS3StorageService s3Storage)
    : IRequestHandler<GetFilesByCreatorIdQuery, ICollection<FileDto>>
{
    public async Task<ICollection<FileDto>> Handle(GetFilesByCreatorIdQuery request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users.FindAsync(request.CreatorId, cancellationToken)
                   ?? throw new NotFoundException("Пользователь не найден.");
        return await GetFiles(request.CreatorId);
    }

    private async Task<List<FileDto>> GetFiles(int creatorId)
    {
        var files = new List<FileDto>();

        foreach (var file in dbContext.Files)
        {
            if (file.CreatorId != creatorId)
            {
                continue;
            }

            var fileDto = new FileDto
            {
                Id = file.Id,
                Url = await s3Storage.GetPreSignedURL(file.Path, TimeSpan.FromHours(1)),
            };
            files.Add(fileDto);
        }

        return files;
    }
}
