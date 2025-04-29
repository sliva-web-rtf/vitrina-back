using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Domain.Project.Page;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;

namespace Vitrina.UseCases.Common.Repositories;

public class ProjectPageRepository : IProjectPageRepository
{
    private readonly IAppDbContext dbContext;
    private readonly DbSet<ProjectPage> pages;

    public ProjectPageRepository(IAppDbContext dbContext)
    {
        this.dbContext = dbContext;
        pages = this.dbContext.ProjectPages;
    }

    public async Task<ProjectPage> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var page = await pages.FindAsync(id, cancellationToken);
        return page ?? throw new NotFoundException($"Page with id = {id} not found");
    }

    public async Task Update(ProjectPage page, CancellationToken cancellationToken)
    {
        if (await pages.FindAsync(page.Id, cancellationToken) != null)
        {
            pages.Update(page);
        }

        throw new NotFoundException("Project page not found");
    }

    public async Task AddAsync(ProjectPage page, CancellationToken cancellationToken) =>
        await pages.AddAsync(page, cancellationToken);

    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        ProjectPage? page;
        if ((page = await pages.FindAsync(id, cancellationToken)) != null)
        {
            pages.Remove(page);
        }

        throw new NotFoundException("Project page not found");
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken) =>
        await dbContext.SaveChangesAsync(cancellationToken);
}
