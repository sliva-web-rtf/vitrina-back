using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.ProjectSphere.GetSpheres;

public class GetSpheresQueryHandler(IAppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetSpheresQuery, ICollection<ResponceSphereDto>>
{
    public async Task<ICollection<ResponceSphereDto>> Handle(GetSpheresQuery request,
        CancellationToken cancellationToken)
    {
        var spheres = await dbContext.ProjectSpheres.ToListAsync(cancellationToken);
        return mapper.Map<ICollection<ResponceSphereDto>>(spheres);
    }
}
