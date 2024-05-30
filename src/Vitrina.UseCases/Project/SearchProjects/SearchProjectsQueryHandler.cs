using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Saritasa.Tools.EntityFrameworkCore.Pagination;
using Vitrina.Domain.Project;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Project.SearchProjects;

/// <summary>
/// Search projects handler.
/// </summary>
internal class SearchProjectsQueryHandler : IRequestHandler<SearchProjectsQuery, ICollection<ShortProjectDto>>
{
    private readonly IAppDbContext dbContext;
    private readonly IMapper mapper;

    public SearchProjectsQueryHandler(IMapper mapper, IAppDbContext dbContext)
    {
        this.mapper = mapper;
        this.dbContext = dbContext;
    }

    public async Task<ICollection<ShortProjectDto>> Handle(SearchProjectsQuery request, CancellationToken cancellationToken)
    {
        var query = dbContext.Projects.ProjectTo<ShortProjectDto>(mapper.ConfigurationProvider);

        if (!string.IsNullOrEmpty(request.Name))
        {
            query = query.Where(p => p.Name.Contains(request.Name));
        }

        if (!string.IsNullOrEmpty(request.Organization))
        {
            query = query.Where(p => p.Client != null && p.Client.Contains(request.Organization));
        }

        if (request.Semester != null)
        {
            var semester = (SemesterEnum)request.Semester;
            query = query.Where(p => p.Semester == semester);
        }

        if (!string.IsNullOrEmpty(request.Period))
        {
            query = query.Where(p => p.Period == request.Period);
        }

        var pagedList = await EFPagedListFactory.FromSourceAsync(query, request.Page, request.PageSize, cancellationToken);

        return pagedList.ToList();
    }
}
