using AutoMapper;
using MediatR;
using Saritasa.Tools.Common.Pagination;
using Saritasa.Tools.EntityFrameworkCore.Pagination;
using Vitrina.Domain.Project;
using Vitrina.Infrastructure.Abstractions.Interfaces;

namespace Vitrina.UseCases.Project.SearchProjects;

/// <summary>
///     Search projects handler.
/// </summary>
internal class SearchProjectsQueryHandler : IRequestHandler<SearchProjectsQuery, PagedList<ShortProjectDto>>
{
    private readonly IAppDbContext dbContext;
    private readonly IMapper mapper;

    public SearchProjectsQueryHandler(IMapper mapper, IAppDbContext dbContext)
    {
        this.mapper = mapper;
        this.dbContext = dbContext;
    }

    public async Task<PagedList<ShortProjectDto>> Handle(SearchProjectsQuery request,
        CancellationToken cancellationToken)
    {
        var query = Queryable
            .AsQueryable<>(dbContext.Projects
                .OrderByDescending(p => p.Priority)
                .Include(p => p.Pages));

        if (!string.IsNullOrEmpty(request.Name))
        {
            query = query.Where(p => p.Name.ToLower().Contains(request.Name.ToLower()));
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

        var pagedList =
            await EFPagedListFactory.FromSourceAsync(query, request.Page, request.PageSize, cancellationToken);
        var result = new List<ShortProjectDto>();
        foreach (var item in pagedList)
        {
            var dto = mapper.Map<ShortProjectDto>(item);
            if (item.PreviewImagePath != null)
            {
                dto.PreviewImagePath = item.PreviewImagePath.Split("/").Last();
            }

            var content = item.Pages.FirstOrDefault();
            if (content != null)
            {
                dto.ImageUrl = content.ImageUrl.Split("/").Last();
            }

            result.Add(dto);
        }

        var newPagedList = PagedListFactory.Create(result, request.Page, request.PageSize, pagedList.TotalCount);
        return newPagedList;
    }
}
