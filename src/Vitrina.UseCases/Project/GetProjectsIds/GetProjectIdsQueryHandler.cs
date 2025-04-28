using MediatR;
using Microsoft.EntityFrameworkCore;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Project.GetProjectsIds;

internal class GetProjectIdsQueryHandler : IRequestHandler<GetProjectIdsQuery, ICollection<int>>
{
    private readonly IAppDbContext dbContext;

    /// <summary>
    ///     Ctr.
    /// </summary>
    /// <param name="dbContext"></param>
    public GetProjectIdsQueryHandler(IAppDbContext dbContext) => this.dbContext = dbContext;

    public async Task<ICollection<int>> Handle(GetProjectIdsQuery request, CancellationToken cancellationToken) =>
        await dbContext.Projects.Select(p => p.Id).ToListAsync(cancellationToken);
}
