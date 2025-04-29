using MediatR;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Project.GetPeriods;

/// <summary>
///     Get periods handler.
/// </summary>
internal class GetPeriodsQueryHandler : IRequestHandler<GetPeriodsQuery, ICollection<string>>
{
    private readonly IAppDbContext dbContext;

    public GetPeriodsQueryHandler(IAppDbContext dbContext) => this.dbContext = dbContext;

    public async Task<ICollection<string>> Handle(GetPeriodsQuery request, CancellationToken cancellationToken)
        => await dbContext.Projects.Select(p => p.Period).Distinct().ToListAsync(cancellationToken);
}
