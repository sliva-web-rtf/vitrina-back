using AutoMapper;
using MediatR;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.ProjectSphere.GetSphereById;

public class GetSphereByIdQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetSphereByIdQuery, ResponceSphereDto>
{
    public async Task<ResponceSphereDto> Handle(GetSphereByIdQuery request, CancellationToken cancellationToken)
    {
        var sphere = await dbContext.ProjectSpheres.FindAsync(request.Id, cancellationToken)
                     ?? throw new NotFoundException(
                         $"The sphere with {nameof(request.Id)} = {request.Id} was not found");
        return mapper.Map<ResponceSphereDto>(sphere);
    }
}
