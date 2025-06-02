using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.ProjectSphere.CreateSphere;

public class CreateSphereCommandHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<CreateSphereCommand, Guid>
{
    public async Task<Guid> Handle(CreateSphereCommand request, CancellationToken cancellationToken)
    {
        var sphereDto = request.SphereDto;
        _ = await dbContext.ProjectSpheres.FirstOrDefaultAsync(projectSphere => projectSphere.Name == sphereDto.Name,
                cancellationToken)
            ?? throw new DomainException($"The sphere with {nameof(sphereDto.Name)} = {sphereDto.Name} already exists");
        var sphere = mapper.Map<Domain.Project.ProjectSphere>(sphereDto);
        dbContext.ProjectSpheres.Add(sphere);
        await dbContext.SaveChangesAsync(cancellationToken);
        return sphere.Id;
    }
}
