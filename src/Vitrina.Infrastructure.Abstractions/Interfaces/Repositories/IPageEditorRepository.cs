using Vitrina.Domain.Project.Page;

namespace Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;

public interface IPageEditorRepository
{
    public Task AddAsync(PageEditor editor, CancellationToken cancellationToken);

    public Task<PageEditor> DeleteAsync(Guid id, CancellationToken cancellationToken);

    public Task SaveChangesAsync(CancellationToken cancellationToken);
}
