using MediatR;
using Vitrina.UseCases.Common.DTO;

namespace Vitrina.UseCases.YandexBucket.Files.GetFilesByCreatorId;

public record GetFilesByCreatorIdQuery(int CreatorId) : IRequest<ICollection<FileDto>>;
