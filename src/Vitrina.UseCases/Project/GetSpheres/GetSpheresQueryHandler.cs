using MediatR;
using Microsoft.EntityFrameworkCore;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Project.GetSpheres;

internal class GetSpheresQueryHandler(IAppDbContext dbContext) : IRequestHandler<GetSpheresQuery, ICollection<string>>
{
    public async Task<ICollection<string>> Handle(GetSpheresQuery request, CancellationToken cancellationToken)
        => await dbContext.Projects
            .Select(project => project.Sphere)
            .Where(sphere => sphere != null)
            .Distinct()
            .ToListAsync(cancellationToken);
}
