using MediatR;
using Microsoft.EntityFrameworkCore;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Project.GetProjectsIds;

internal class GetProjectIdsQueryHandler(IAppDbContext dbContext)
    : IRequestHandler<GetProjectIdsQuery, ICollection<int>>
{
    public async Task<ICollection<int>> Handle(GetProjectIdsQuery request, CancellationToken cancellationToken) =>
        await dbContext.Projects
            .Select(project => project.Id)
            .ToListAsync(cancellationToken);
}
