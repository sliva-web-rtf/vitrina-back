using MediatR;
using Microsoft.EntityFrameworkCore;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Project.GetSpheres;

internal class GetSpheresQueryHandler : IRequestHandler<GetSpheresQuery, ICollection<string>>
{
    private readonly IAppDbContext dbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetSpheresQueryHandler(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<ICollection<string>> Handle(GetSpheresQuery request, CancellationToken cancellationToken)
        => await dbContext.Projects.Select(p => p.Sphere).Distinct().ToListAsync(cancellationToken);
}
