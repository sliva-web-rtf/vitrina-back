using MediatR;
using Microsoft.EntityFrameworkCore;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Project.GetTypes;

internal class GetTypesQueryHandler : IRequestHandler<GetTypesQuery, ICollection<string>>
{
    private readonly IAppDbContext dbContext;

    /// <summary>
    /// Constructor.
    /// </summary>
    public GetTypesQueryHandler(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<ICollection<string>> Handle(GetTypesQuery request, CancellationToken cancellationToken)
        => await dbContext.Projects.Select(p => p.Type).Distinct().ToListAsync(cancellationToken);
}
