using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Domain.Exceptions;
using Vitrina.Domain.Project.Page;
using Vitrina.Infrastructure.Abstractions.Interfaces;
using Vitrina.Infrastructure.Abstractions.Interfaces.Repositories;

namespace Vitrina.Infrastructure.DataAccess.Repositories;

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

    public async Task AddAsync(ProjectPage page, CancellationToken cancellationToken) =>
        await pages.AddAsync(page, cancellationToken);

    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        ProjectPage? page;
        if ((page = await pages.FindAsync(id, cancellationToken)) is null)
        {
            throw new NotFoundException("Project page not found");
        }

        pages.Remove(page);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken) =>
        await dbContext.SaveChangesAsync(cancellationToken);
}
