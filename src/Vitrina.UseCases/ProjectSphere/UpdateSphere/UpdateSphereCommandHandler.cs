using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.ProjectSphere.UpdateSphere;

public class UpdateSphereCommandHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<UpdateSphereCommand, ResponceSphereDto>
{
    public async Task<ResponceSphereDto> Handle(UpdateSphereCommand request, CancellationToken cancellationToken)
    {
        var sphere = await dbContext.ProjectSpheres.FindAsync(request.Id, cancellationToken)
                     ?? throw new NotFoundException($"Sphere with id = {request.Id} not found");
        var sphereDto = mapper.Map<RequestSphereDto>(sphere);
        request.PatchDocument.ApplyTo(sphereDto);
        _ = await dbContext.ProjectSpheres.FirstOrDefaultAsync(existingSphere =>
                existingSphere.Id != request.Id && existingSphere.Name == sphereDto.Name, cancellationToken)
            ?? throw new DomainException($"The sphere with {nameof(sphere.Name)} = {sphereDto.Name} already exists");
        mapper.Map(sphereDto, sphere);
        await dbContext.SaveChangesAsync(cancellationToken);
        return mapper.Map<ResponceSphereDto>(sphere);
    }
}
