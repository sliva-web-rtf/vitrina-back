using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saritasa.RedMan.Infrastructure.Abstractions.Interfaces;
using Saritasa.Tools.Common.Pagination;
using Saritasa.Tools.Common.Utils;
using Saritasa.Tools.EntityFrameworkCore.Pagination;

namespace Saritasa.RedMan.UseCases.Store.SearchProducts;

/// <summary>
/// Handler for <see cref="SearchProductsQuery" />.
/// </summary>
internal class SearchProductsQueryHandler : IRequestHandler<SearchProductsQuery, PagedList<ProductSummaryDto>>
{
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="appDbContext">Database context.</param>
    /// <param name="mapper">Automapper instance.</param>
    public SearchProductsQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        this.appDbContext = appDbContext;
        this.mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<PagedList<ProductSummaryDto>> Handle(SearchProductsQuery request, CancellationToken cancellationToken)
    {
        var query = appDbContext.Products.Include(p => p.CreatedByUser).AsQueryable();

        if (!string.IsNullOrEmpty(request.NameTerm))
        {
            query = query.Where(q => q.Name.StartsWith(request.NameTerm));
        }

        query = CollectionUtils.OrderMultiple(
            query,
            OrderParsingDelegates.ParseSeparated(request.OrderBy.ToLower()),
            ("id", x => x.Id),
            ("createdat", x => x.CreatedAt),
            ("name", x => x.Name)
        );

        var pagedList = await EFPagedListFactory.FromSourceAsync(query, request.Page, request.PageSize, cancellationToken);
        return pagedList.Convert(p => mapper.Map<ProductSummaryDto>(p));
    }
}
