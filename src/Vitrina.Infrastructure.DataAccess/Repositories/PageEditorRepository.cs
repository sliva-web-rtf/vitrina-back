using Microsoft.EntityFrameworkCore;
using Vitrina.Domain.Project.Page;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;

namespace Vitrina.Infrastructure.DataAccess.Repositories;

public class PageEditorRepository : IPageEditorRepository
{
    private readonly IAppDbContext dbContext;
    private readonly DbSet<PageEditor> editors;

    public PageEditorRepository(IAppDbContext context)
    {
        this.dbContext = context;
        editors = context.PageEditors;
    }

    public async Task AddAsync(PageEditor editor, CancellationToken cancellationToken)
    {
        await editors.AddAsync(editor, cancellationToken);
    }

    public Task<PageEditor> DeleteAsync(Guid id, CancellationToken cancellationToken) => throw new NotImplementedException();

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
