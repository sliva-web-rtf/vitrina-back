using MediatR;
using Microsoft.EntityFrameworkCore;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Project.GetTypes;

internal class GetTypesQueryHandler(IAppDbContext dbContext) : IRequestHandler<GetTypesQuery, ICollection<string>>
{
    public async Task<ICollection<string>> Handle(GetTypesQuery request, CancellationToken cancellationToken)
        => await dbContext.Projects
            .Select(project => project.Thematics)
            .Where(type => type != null)
            .Distinct()
            .ToListAsync(cancellationToken);
}
