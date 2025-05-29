using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Domain.Project.Page.Editor;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;

namespace Vitrina.Infrastructure.DataAccess.Repositories;

public class PageEditorRepository(IAppDbContext context) : IPageEditorRepository
{
    private readonly DbSet<PageEditor> editors = context.PageEditors;

    public async Task AddAsync(PageEditor editor, CancellationToken cancellationToken) =>
        await editors.AddAsync(editor, cancellationToken);

    public async Task<PageEditor> DeleteAsync(Guid id, Guid pageId, CancellationToken cancellationToken)
    {
        var editor = await editors.FindAsync(id, cancellationToken) ??
                     throw new NotFoundException($"Editor with id = {id} not found");
        if (editor.PageId != pageId)
        {
            throw new DomainException($"The page of the project with id = {pageId} has no editor with id = {id}");
        }

        editors.Remove(editor);
        return editor;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken) =>
        await context.SaveChangesAsync(cancellationToken);
}
