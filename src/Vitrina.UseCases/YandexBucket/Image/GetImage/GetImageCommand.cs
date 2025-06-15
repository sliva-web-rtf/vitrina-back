using MediatR;
using Vitrina.UseCases.YandexBucket.Image.Dto;

namespace Vitrina.UseCases.YandexBucket.Image.GetImage;

public record GetImageCommand(Guid Id) : IRequest<ImageDto>;
