using Vitrina.Domain.Project.Page;

namespace Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;

public interface IProjectPageRepository
{
    public Task<ProjectPage> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    public void Update(ProjectPage page, CancellationToken cancellationToken);

    public Task AddAsync(ProjectPage page, CancellationToken cancellationToken);

    public void Delete(ProjectPage page, CancellationToken cancellationToken);

    public Task SaveChangesAsync(CancellationToken cancellationToken);
}
