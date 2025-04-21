using Vitrina.Domain.Project.Page;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;

namespace Vitrina.UseCases.Common.Repositories;

public class ProjectPageRepository(IAppDbContext dbContext) : IRepository<ProjectPage>
{
    public async Task<ProjectPage> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await dbContext.Contents.FindAsync(id, cancellationToken);
    }

    public Task UpdateAsync(ProjectPage entity, CancellationToken cancellationToken) => throw new NotImplementedException();
}
